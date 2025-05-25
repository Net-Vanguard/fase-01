namespace FCG.Domain.Entities;

public class Game
{
    public Guid Id { get; private set; }
    public string Title { get; private set; } = string.Empty;
    private readonly List<Guid> _playerIds = new();
    public IReadOnlyCollection<Guid> PlayerIds => _playerIds.AsReadOnly();

    private Game() { }

    public Game(string title)
    {
        SetTitle(title);
        Id = Guid.NewGuid();
    }

    public void SetTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("O título do jogo não pode ser vazio.");
        Title = title;
    }

    /// <summary>
    /// Altera o título do jogo respeitando as regras de domínio.
    /// </summary>
    public void ChangeTitle(string title)
    {
        SetTitle(title);
    }

    public void AddPlayer(Guid userId)
    {
        if (_playerIds.Contains(userId))
            throw new InvalidOperationException("Usuário já está associado a este jogo.");
        _playerIds.Add(userId);
    }

    public void RemovePlayer(Guid userId)
    {
        if (!_playerIds.Contains(userId))
            throw new InvalidOperationException("Usuário não está associado a este jogo.");
        _playerIds.Remove(userId);
    }
}
