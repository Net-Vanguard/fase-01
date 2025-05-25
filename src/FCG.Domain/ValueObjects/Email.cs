using System.Text.RegularExpressions;

namespace FCG.Domain.ValueObjects;

public record Email
{
    private static readonly Regex Pattern = new("^[^@\\s]+@[^@\\s]+\\.[^@\\s]+$", RegexOptions.Compiled);
    public string Address { get; }

    public Email(string address)
    {
        if (string.IsNullOrWhiteSpace(address) || !Pattern.IsMatch(address))
            throw new ArgumentException("Formato de e-mail inválido.", nameof(address));
        Address = address;
    }

    public override string ToString() => Address;
}