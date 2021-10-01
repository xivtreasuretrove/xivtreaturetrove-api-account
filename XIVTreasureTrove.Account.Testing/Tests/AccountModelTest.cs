
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using XIVTreasureTrove.Account.Domain.Models;
using Xunit;

namespace XIVTreasureTrove.Account.Testing.Tests
{
    public class AccountModelTest
    {
        public static readonly IEnumerable<object[]> Accounts = new List<object[]>
        {
            new object[]
            {
                new AccountModel()
                {
                    EntityId = 0,
                    Username = "RoguishTraveler",
                    Email = "roguishtraveler@gmail.com"
                }
            }
        };

        [Theory]
        [MemberData(nameof(Accounts))]
        public void Test_Create_AccountModel(AccountModel account)
        {
            var validationContext = new ValidationContext(account);
            var actual = Validator.TryValidateObject(account, validationContext, null, true);

            Assert.True(actual);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="email"></param>
        [Theory]
        [InlineData("RoguishTraveler", "NotAnEmail")]
        public void Test_Create_AccountModel_BadEmail(string username, string email)
        {
            AccountModel account = new AccountModel(username, email);

            var validationContext = new ValidationContext(account);
            var actual = Validator.TryValidateObject(account, validationContext, null, true);

            Assert.False(actual);
        }

        [Theory]
        [MemberData(nameof(Accounts))]
        public void Test_Validate_AccountModel(AccountModel account)
        {
            var validationContext = new ValidationContext(account);

            Assert.Empty(account.Validate(validationContext));
        }
    }
}
