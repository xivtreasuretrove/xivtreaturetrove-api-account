using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XIVTreasureTrove.Account.Domain.Models;

namespace XIVTreasureTrove.Account.Context
{
    /// <summary>
    /// The AccountContext class
    /// </summary>
    public class AccountContext : DbContext
    {
        public DbSet<AccountModel> Accounts { get; set; }

        public AccountContext(DbContextOptions<AccountContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountModel>().HasKey(e => e.EntityId);

            OnDataSeeding(modelBuilder);
        }

        private void OnDataSeeding(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountModel>().HasData(new AccountModel()
            {
                EntityId = 1,
                Username = "RoguishTraveler",
                Email = "roguishtraveler@gmail.com"
            });
        }
    }
}
