using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XIVTreasureTrove.Account.Domain.Interfaces;
using XIVTreasureTrove.Account.Domain.Models;

namespace XIVTreasureTrove.Account.Context.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AccountContext _context;

        public IRepository<AccountModel> Account { get; }

        public UnitOfWork(AccountContext context)
        {
            _context = context;

            Account = new Repository<AccountModel>(context);
        }

        public Task<int> CommitAsync() => _context.SaveChangesAsync();
    }
}
