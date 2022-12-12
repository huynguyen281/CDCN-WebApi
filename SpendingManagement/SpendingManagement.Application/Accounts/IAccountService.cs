using SpendingManagement.Share.ApiResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpendingManagement.Application.Accounts
{
    public interface IAccountService
    {
        Task<ApiResultBase<InfoAccountResponse>> AuthenticateAsync(LoginRequest request);
        Task<ApiResultBase<string>> RegisterAsync(RegisterRequest request);
        Task<ApiResultBase<string>> ChangePasswordAsync(Guid userId, string newPassword);
        Task<ApiResultBase<InfoAccountResponse>> UpdateInfoAccountAsync(UpdateInfoAccountRequest request);
    }
}
