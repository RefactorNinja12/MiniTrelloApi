namespace MiniTrello.Dtos
{
    public class CardDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty ;
        public int Position { get; set; }
        public int BoardListId { get; set; }
    }
}
