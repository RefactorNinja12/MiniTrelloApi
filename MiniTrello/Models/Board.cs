namespace MiniTrello.Models
{
    public class Board
    {
        public string UserId { get; set; } = string.Empty;
        public User user { get; set; }
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<BoardList> BoardLists { get; set; } = new();

       
    }
}
