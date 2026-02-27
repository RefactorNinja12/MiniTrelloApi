using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MiniTrello.Models;
using MiniTrello.Services;
using System;

using System.Text;


var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<MiniTrelloDbContext>()
    .AddDefaultTokenProviders();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ClockSkew = TimeSpan.Zero,
            IssuerSigningKey =
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

builder.Services.AddControllers();
builder.Services.AddDbContext<MiniTrelloDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http, // Denna gör att "Bearer" läggs till automatiskt
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Klistra in endast din JWT-token nedan."
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});



builder.Services.AddScoped<IBoardService, BoardService>();
builder.Services.AddScoped<IBoardListService, BoardListService>();
builder.Services.AddScoped<ICardService, CardService>();


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var logger = services.GetRequiredService<ILogger<Program>>();
    var context = services.GetRequiredService<MiniTrelloDbContext>();

    // Retry-logik: Försök 5 gånger med 2 sekunders paus
    for (int i = 0; i < 5; i++)
    {
        try
        {
            logger.LogInformation("Försöker köra databasmigrationer (försök {Attempt})...", i + 1);
            context.Database.Migrate();
            logger.LogInformation("Migrationer klara!");
            break; // Avbryt loopen om det lyckas
        }
        catch (Exception ex)
        {
            if (i == 4) // Om sista försöket misslyckas
            {
                logger.LogCritical(ex, "Kunde inte ansluta till databasen efter 5 försök.");
                throw;
            }
            logger.LogWarning("Databasen är inte redo än, väntar 2 sekunder...");
            System.Threading.Thread.Sleep(2000);
        }
    }
}


app.UseAuthentication();
app.UseAuthorization();


    app.UseSwagger();
    app.UseSwaggerUI();


app.MapControllers();

app.Run();