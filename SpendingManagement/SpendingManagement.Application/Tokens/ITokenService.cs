using SpendingManagement.Application.Accounts;
using SpendingManagement.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpendingManagement.Application.Tokens
{
    public interface ITokenService
    {
        Task<string> CreateTokenAsync(LoginRequest request);
    }
}
