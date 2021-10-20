using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XIVTreasureTrove.Account.Domain.Interfaces;
using XIVTreasureTrove.Account.Domain.Models;
using XIVTreasureTrove.Account.Service.ResponseObjects;

namespace XIVTreasureTrove.Account.Service.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [ApiVersion("0.0")]
    [EnableCors("Public")]
    [Route("rest/account/{version:apiVersion}/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="unitofWork"></param>
        public AccountController(ILogger<AccountController> logger, IUnitOfWork unitofWork)
        {
            _logger = logger;
            _unitOfWork = unitofWork;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("DeleteAccount/{email}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAccount(string email)
        {
            try
            {
                var accountModel = (await _unitOfWork.Account.SelectAsync(e => e.Email == email)).FirstOrDefault();

                await _unitOfWork.Account.DeleteAsync(accountModel.EntityId);
                await _unitOfWork.CommitAsync();

                return Ok(MessageObject.Success);
            }
            catch (Exception error)
            {
                _logger.LogError(error, error.Message);

                return NotFound(new ErrorObject($"Account with email {email} does not exist"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAccounts")]
        [ProducesResponseType(typeof(IEnumerable<AccountModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAccounts()
        {
            return Ok(await _unitOfWork.Account.SelectAsync());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAccountByEmail/{email}")]
        [ProducesResponseType(typeof(AccountModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAccountByEmail(string email)
        {
            var accountModel = (await _unitOfWork.Account.SelectAsync(e => e.Email == email)).FirstOrDefault();

            if (accountModel == null)
            {
                return NotFound(new ErrorObject($"Account with email {email} does not exist."));
            }

            return Ok(accountModel);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddAccount")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        public async Task<IActionResult> AddAccount([FromBody] AccountModel account)
        {
            await _unitOfWork.Account.InsertAsync(account);
            await _unitOfWork.CommitAsync();

            return Accepted(account);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("UpdateAccount")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAccount([FromBody] AccountModel account)
        {
            try
            {
                _unitOfWork.Account.Update(account);

                await _unitOfWork.CommitAsync();

                return Accepted(account);
            }
            catch (Exception error)
            {
                _logger.LogError(error, error.Message);

                return NotFound(new ErrorObject($"Account with ID number {account.EntityId} does not exist"));
            }
        }
    }
}
