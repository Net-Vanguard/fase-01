using FCG.Domain.ValueObjects;

namespace FCG.Domain.Entities;

public class User
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public Email Email { get; private set; } = null!;
    public Password Password { get; private set; } = null!;

    // Construtor privado para EF Core
    private User() { }

    // Fábrica de criação com validações
    public User(string name, Email email, Password password)
    {
        SetName(name);
        Email = email ?? throw new ArgumentNullException(nameof(email));
        Password = password ?? throw new ArgumentNullException(nameof(password));
        Id = Guid.NewGuid();
    }

    // Método de domínio para alterar nome,
    // garantindo validação de regra de negócio
    public void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("O nome não pode ser vazio.");
        Name = name;
    }

    /// <summary>
    ///  
    /// </summary>
    /// <param name="newPassword"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public void ChangePassword(Password newPassword)
    {
        Password = newPassword ?? throw new ArgumentNullException(nameof(newPassword));
    }

    /// <summary>
    /// Altera o nome respeitando invariantes de domínio.
    /// </summary>
    public void ChangeName(string name)
    {
        SetName(name);
    }
}
