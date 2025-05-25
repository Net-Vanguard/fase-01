namespace FCG.Domain.Interfaces;

public interface IUnitOfWork
{
    /// <summary>
    /// Persiste as alterações no contexto.
    /// </summary>
    /// <param name="cancellationToken">Token de cancelamento opcional.</param>
    /// <returns>Número de registros afetados.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
