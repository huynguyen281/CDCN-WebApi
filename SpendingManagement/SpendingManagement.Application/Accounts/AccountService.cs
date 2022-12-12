using DbFactory;
using Microsoft.AspNetCore.Identity;
using SpendingManagement.Application.Tokens;
using SpendingManagement.Data.Entities;
using SpendingManagement.Repository.Budgets;
using SpendingManagement.Share.ApiResults;
using System.Globalization;

namespace SpendingManagement.Application.Accounts
{
    public class AccountService : IAccountService
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IBudgetRepository _budgetRepository;
        public AccountService(SignInManager<AppUser> signInManager, ITokenService tokenService,
                                    UserManager<AppUser> userManager, IBudgetRepository budgetRepository)
        {
            _signInManager = signInManager;
            _tokenService = tokenService;
            _userManager = userManager;
            _budgetRepository = budgetRepository;   
        }

        public async Task<ApiResultBase<InfoAccountResponse>> AuthenticateAsync(LoginRequest request)
        {
            var resultFindUserName = await _userManager.FindByNameAsync(request.UserName)
                ?? await _userManager.FindByEmailAsync(request.UserName);
            if (resultFindUserName is null)
            {
                return new ApiErrorResult<InfoAccountResponse>(message: "Tài khoản không tồn tại!");
            }
            var result = await _signInManager.PasswordSignInAsync(resultFindUserName.UserName, request.Password, false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                var token = await _tokenService.CreateTokenAsync(request);
                var infoAccountResponse = new InfoAccountResponse()
                {
                    Id = resultFindUserName.Id,
                    DateOfBirth = resultFindUserName.DateOfBirth.ToString("dd-MM-yyyy"),
                    Email = resultFindUserName.Email,
                    UserName = resultFindUserName.UserName,
                    Gender = resultFindUserName.Gender,
                    ImageURL = resultFindUserName.PhotoUrl,
                    Name = resultFindUserName.Name,
                    Token = token
                };    
                return new ApiSuccessResult<InfoAccountResponse>(resultObj: infoAccountResponse, message: "Đăng nhập thành công!");
            }
            return new ApiErrorResult<InfoAccountResponse>(message: "Mật khẩu không đúng!");
        }

        public async Task<ApiResultBase<string>> RegisterAsync(RegisterRequest request)
        {
            var resultFindUserName = await _userManager.FindByNameAsync(request.UserName)
                ?? await _userManager.FindByEmailAsync(request.Email);
            if (resultFindUserName != null)
            {
                return new ApiErrorResult<string>(message: "Tên đăng nhập hoặc email này đã được đăng ký!");
            }
            var appUser = new AppUser()
            {
                Id = Guid.NewGuid(),
                Email = request.Email,
                NormalizedEmail = request.Email.ToUpper(),
                EmailConfirmed = true,
                DateOfBirth = DateTime.Now,
                Name = request.Name,
                UserName = request.UserName,
                NormalizedUserName = request.UserName.ToUpper(),
                PhotoUrl = String.Empty,
                PhoneNumber = String.Empty,
                PhoneNumberConfirmed = true,
                ConcurrencyStamp = String.Empty,
            };
            var result = await _userManager.CreateAsync(appUser, request.Password);
            if(result.Succeeded)
            {
                _budgetRepository.Create(new Budget() 
                {
                    Id = Guid.NewGuid(),
                    AppUserId = appUser.Id,
                });
                return new ApiSuccessResult<string>(message: "Đăng ký thành công!");
            }
            return new ApiErrorResult<string>(message: "Đăng ký không thành công");
        }
        public async Task<ApiResultBase<string>> ChangePasswordAsync(Guid userId, string newPassword)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if(user is not null)
            {
                string token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var result = await _userManager.ResetPasswordAsync(user, token, newPassword);
                if(result.Succeeded)
                {
                    return new ApiSuccessResult<string>(message: "Đổi mật khẩu thành công");
                }
                return new ApiErrorResult<string>(message: "Đổi mật khẩu không thành công");
            }
            return new ApiErrorResult<string>(message: "Đổi mật khẩu không thành công");
        }
        public async Task<ApiResultBase<InfoAccountResponse>> UpdateInfoAccountAsync(UpdateInfoAccountRequest request)
        {
            var user = await _userManager.FindByIdAsync(request.Id.ToString());
            if(user is not null)
            {
                user.Name = request.Name;
                var result = await _userManager.UpdateAsync(user);
                if(result.Succeeded)
                {
                    var infoAccountResponse = new InfoAccountResponse()
                    {
                        Id = user.Id,
                        DateOfBirth = user.DateOfBirth.ToString("dd-MM-yyyy"),
                        Email = user.Email,
                        Gender = user.Gender,
                        Name = user.Name,
                        UserName = user.UserName,
                        Token = null,
                        ImageURL = null
                    };
                    return new ApiSuccessResult<InfoAccountResponse>(message: "Cập nhật thông tin thành công", resultObj: infoAccountResponse);
                }
                return new ApiErrorResult<InfoAccountResponse>(message: "Cập nhật thông tin không thành công");
            }
            return new ApiErrorResult<InfoAccountResponse>(message: "Người dùng không tồn tại");
        }
    }
}