using Microsoft.EntityFrameworkCore;
using XIVTreasureTrove.Account.Context;
using XIVTreasureTrove.Account.Context.Repositories;
using XIVTreasureTrove.Account.Domain.Models;
using XIVTreasureTrove.Account.Testing.Integrations;
using Xunit;

namespace XIVTreasureTrove.Account.Testing.Tests
{
    public class RepositoryTest : SqliteIntegration
    {
        private readonly AccountModel _account = new AccountModel() { EntityId = 1, Username = "RoguishTraveler", Email = "rogusishtraveler@gmail.com" };

        [Fact]
        public async void Test_Repository_DeleteAsync()
        {
            using var ctx = new AccountContext(options);

            var accounts = new Repository<AccountModel>(ctx);
            var account = await ctx.Accounts.FirstAsync();

            await accounts.DeleteAsync(account.EntityId);

            Assert.Equal(EntityState.Deleted, ctx.Entry(account).State);
        }

        [Fact]
        public async void Test_Repository_InsertAsync()
        {
            using var ctx = new AccountContext(options);

            var accounts = new Repository<AccountModel>(ctx);

            await accounts.InsertAsync(_account);

            Assert.Equal(EntityState.Added, ctx.Entry(_account).State);
        }

        [Fact]
        public async void Test_Repository_SelectAsync()
        {
            using var ctx = new AccountContext(options);

            var accounts = new Repository<AccountModel>(ctx);
            var actual = await accounts.SelectAsync();

            Assert.NotEmpty(actual);
        }

        [Theory]
        [InlineData(1)]
        public async void Test_Repository_SelectAsync_ById(int id)
        {
            using var ctx = new AccountContext(options);

            var accounts = new Repository<AccountModel>(ctx);
            var actual = await accounts.SelectAsync(e => e.EntityId == id);

            Assert.NotNull(actual);
        }

        [Theory]
        [InlineData("email")]
        public async void Test_Repository_Update(string email)
        {
            using var ctx = new AccountContext(options);

            var accounts = new Repository<AccountModel>(ctx);
            var account = await ctx.Accounts.FirstAsync();

            account.Email = email;
            accounts.Update(account);

            var result = ctx.Accounts.Find(account.EntityId);

            Assert.Equal(account.Email, result.Email);
            Assert.Equal(EntityState.Modified, ctx.Entry(result).State);
        }
    }
}
