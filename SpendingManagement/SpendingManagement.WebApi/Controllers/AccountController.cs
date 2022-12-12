using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpendingManagement.Application.Accounts;

namespace SpendingManagement.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(LoginRequest request)
        {
            try
            {
                return Ok(await _accountService.AuthenticateAsync(request));
            }
            catch (Exception)
            {
                throw new Exception("Some thing has gone wrong!");
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(RegisterRequest request)
        {
            try
            {
                return Ok(await _accountService.RegisterAsync(request));
            }
            catch (Exception)
            {
                throw new Exception("Some thing has gone wrong!");
            }
        }

        [HttpPut("change-password/{id}")]
        public async Task<IActionResult> ChangePasswordAsync(Guid id, PasswordChangeRequest request)
        {
            try
            {
                return Ok(await _accountService.ChangePasswordAsync(id, request.Password));
            }
            catch (Exception)
            {
                throw new Exception("Some thing has gone wrong!");
            } 
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInfoAccountAsync(Guid id, UpdateInfoAccountRequest request)
        {
            if(id != request.Id)
            {
                return new BadRequestResult();
            }
            try
            {
                return Ok(await _accountService.UpdateInfoAccountAsync(request));
            }
            catch (Exception)
            {
                throw new Exception("Some thing has gone wrong!");
            }
        }
    }
}
