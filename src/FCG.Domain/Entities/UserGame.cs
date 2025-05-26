namespace FCG.Domain.Entities
{
    /// <summary>
    /// Representa a aquisição de um jogo por um usuário.
    /// </summary>
    public class UserGame
    {
        public Guid UserId { get; private set; }
        public Guid GameId { get; private set; }
        public DateTime AcquiredAt { get; private set; }

        // Construtor param-less para o EF Core
        private UserGame() { }

        // Construtor de criação
        public UserGame(Guid userId, Guid gameId)
        {
            UserId = userId;
            GameId = gameId;
            AcquiredAt = DateTime.UtcNow;
        }
    }
}
