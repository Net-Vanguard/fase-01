using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace FCG.Domain.ValueObjects;

/// <summary>
/// Value Object que encapsula regras de validação e criptografia de senha.
/// </summary>
public record Password
{
    private const int SaltSize = 16; // 128 bits
    private const int KeySize = 32;  // 256 bits
    private const int Iterations = 10000;

    private static readonly Regex SecurePattern = new(
           @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$",
           RegexOptions.Compiled
       );

    public string Hashed { get; private set; }
    public string Salt { get; private set; }

    public Password(string plainPassword)
    {
        if (string.IsNullOrWhiteSpace(plainPassword) || !SecurePattern.IsMatch(plainPassword))
            throw new ArgumentException(
                "Senha inválida. Deve ter mínimo de 8 caracteres, " +
                "incluindo letras maiúsculas, minúsculas, números e caracteres especiais.",
                nameof(plainPassword)
            );

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
    public virtual bool Verify(string plainPassword)
    {
        var saltBytes = Convert.FromBase64String(Salt);
        var attemptedHash = HashPassword(plainPassword, saltBytes);
        return attemptedHash == Hashed;
    }
}