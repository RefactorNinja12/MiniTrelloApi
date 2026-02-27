

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MiniTrello.Models
{
    public class MiniTrelloDbContext : IdentityDbContext<User>
    {
        public MiniTrelloDbContext(DbContextOptions<MiniTrelloDbContext> options) : base(options) { }

        public DbSet<Board> Boards { get; set; }
        public DbSet<BoardList> BoardLists { get; set; }
        public DbSet<Card> Cards { get; set; }
    }
}
