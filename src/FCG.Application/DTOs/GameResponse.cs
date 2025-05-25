namespace FCG.Application.DTOs
{
    public class GameResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public IEnumerable<Guid> PlayerIds { get; set; } = new List<Guid>();
    }
}
