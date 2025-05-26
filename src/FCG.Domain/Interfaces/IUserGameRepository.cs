using FCG.Domain.Entities;

namespace FCG.Domain.Interfaces
{
    /// <summary>
    /// Contrato para persistir aquisições de jogos.
    /// </summary>
    public interface IUserGameRepository
    {
        Task AddAsync(UserGame userGame);
        IUnitOfWork UnitOfWork { get; }
    }
}
