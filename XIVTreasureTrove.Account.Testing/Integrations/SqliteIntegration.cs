using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using XIVTreasureTrove.Account.Context;

namespace XIVTreasureTrove.Account.Testing.Integrations
{
    public abstract class SqliteIntegration
    {
        private readonly SqliteConnection _connection;
        protected readonly DbContextOptions<AccountContext> options;

        public SqliteIntegration()
        {
            _connection = new SqliteConnection("Data Source=:memory:");
            options = new DbContextOptionsBuilder<AccountContext>().UseSqlite(_connection).Options;

            _connection.Open();

            using(var ctx = new AccountContext(options))
            {
                ctx.Database.EnsureCreated();
            }
        }
    }
}
