namespace MiniTrello.Models
{
    public class Board
    {
       

        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<BoardList> BoardLists { get; set; } = new();

       
    }
}
