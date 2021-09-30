using System.Threading.Tasks;
using XIVTreasureTrove.Account.Domain.Models;

namespace XIVTreasureTrove.Account.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<AccountModel> Account { get; }

        Task<int> CommitAsync();
    }
}
