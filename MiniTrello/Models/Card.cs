namespace MiniTrello.Models
{
    public class Card
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public int BoardListId { get; set; }

        public BoardList BoardList { get; set; }
        public int Position { get; set; }
    }
}
