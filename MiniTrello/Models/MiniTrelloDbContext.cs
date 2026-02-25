using Microsoft.EntityFrameworkCore;

namespace MiniTrello.Models
{
    public class MiniTrelloDbContext : DbContext
    {
        public MiniTrelloDbContext(DbContextOptions<MiniTrelloDbContext> options) : base(options) { }

        public DbSet<Board> Boards { get; set; }
        public DbSet<BoardList> BoardLists { get; set; }
        public DbSet<Card> Cards { get; set; }
    }
}
