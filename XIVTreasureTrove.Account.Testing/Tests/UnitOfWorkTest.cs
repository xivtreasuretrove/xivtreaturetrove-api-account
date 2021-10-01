using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XIVTreasureTrove.Account.Context;
using XIVTreasureTrove.Account.Context.Repositories;
using XIVTreasureTrove.Account.Testing.Integrations;
using Xunit;

namespace XIVTreasureTrove.Account.Testing.Tests
{
    public class UnitOfWorkTest : SqliteIntegration
    {
        [Fact]
        public async void Test_UnitOfWork_CommitAsync()
        {
            using var ctx = new AccountContext(options);
            var unitOfWork = new UnitOfWork(ctx);
            var actual = await unitOfWork.CommitAsync();

            Assert.NotNull(unitOfWork.Account);
            Assert.Equal(0, actual);
        }
    }
}
