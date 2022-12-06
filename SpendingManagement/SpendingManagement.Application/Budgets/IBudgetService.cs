using SpendingManagement.Data.Entities;
using SpendingManagement.Share.ApiResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpendingManagement.Application.Budgets
{
    public interface IBudgetService
    {
        ApiResultBase<BudgetResponse>? GetBudgetOfUser(Guid idUser);
        ApiResultBase<BudgetResponse> UpdateBudgetOfUser(BudgetUpdateRequest request, Guid userId);
    }
}
