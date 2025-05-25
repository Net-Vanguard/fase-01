using System.Security.Cryptography;

namespace FCG.Domain.ValueObjects;

/// <summary>
/// Value Object que encapsula regras de validação e criptografia de senha.
/// </summary>
public record Password
{
    private const int SaltSize = 16; // 128 bits
    private const int KeySize = 32;  // 256 bits
    private const int Iterations = 10000;

    public string Hashed { get; private set; }
    public string Salt { get; private set; }

    public Password(string plainPassword)
    {
        if (string.IsNullOrWhiteSpace(plainPassword) || plainPassword.Length < 6)
            throw new ArgumentException("Senha deve ter no mínimo 6 caracteres.", nameof(plainPassword));

        using var rng = RandomNumberGenerator.Create();
        var saltBytes = new byte[SaltSize];
        rng.GetBytes(saltBytes);
        Salt = Convert.ToBase64String(saltBytes);

        Hashed = HashPassword(plainPassword, saltBytes);
    }

    /// <summary>
    /// 3) Construtor usado pelo EF Core, cujos parâmetros batem nos nomes das propriedades.
    /// </summary>
    public Password(string hashed, string salt)
    {
        Hashed = hashed ?? throw new ArgumentNullException(nameof(hashed));
        Salt = salt ?? throw new ArgumentNullException(nameof(salt));
    }
    private static string HashPassword(string password, byte[] saltBytes)
    {
        using var deriveBytes = new Rfc2898DeriveBytes(password, saltBytes, Iterations, HashAlgorithmName.SHA256);
        var key = deriveBytes.GetBytes(KeySize);
        return Convert.ToBase64String(key);
    }

    /// <summary>
    /// Verifica se a senha informada corresponde à senha armazenada.
    /// </summary>
    public bool Verify(string plainPassword)
    {
        var saltBytes = Convert.FromBase64String(Salt);
        var attemptedHash = HashPassword(plainPassword, saltBytes);
        return attemptedHash == Hashed;
    }
}