using Microsoft.EntityFrameworkCore;
using SpendingManagement.Repository.Budgets;
using SpendingManagement.Repository.Expenditures;
using SpendingManagement.Share.ApiResults;
using SpendingManagement.Share.NameUnitTimeGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpendingManagement.Application.Budgets
{
    public class BudgetService : IBudgetService
    {
        private readonly IBudgetRepository _budgetRepository;
        private readonly IExpenditureRepository _expenditureRepository;
        public BudgetService(IBudgetRepository budgetRepository, IExpenditureRepository expenditureRepository)
        {
            _budgetRepository = budgetRepository;
            _expenditureRepository = expenditureRepository;
        }

        public ApiResultBase<BudgetResponse>? GetBudgetOfUser(Guid userId)
        {
            var budget = _budgetRepository.GetBudget(userId);
            if(budget is null)
            {
                return new ApiErrorResult<BudgetResponse>(message: "Không tìm thấy người dùng!");
            }
            var expenditures = _expenditureRepository.GetAllExpenditures().Where(x => x.Category.TypeOfCategory == Share.TypeOfCategory.CategoryType.Out);
            var myDate = DateTime.Now;
            switch (budget.UnitTime)
            {
                case Share.UnitTimes.UnitTime.Day:
                    expenditures = expenditures.Where(x => x.DateCreate.Date == DateTime.Now.Date);
                    break;
                case Share.UnitTimes.UnitTime.Month:
                    var startOfMonth = new DateTime(myDate.Year, myDate.Month, 1);
                    var endOfMonth = startOfMonth.AddMonths(1).AddDays(-1);
                    expenditures = expenditures.Where(x => x.DateCreate >= startOfMonth && x.DateCreate <= endOfMonth);
                    break;
                case Share.UnitTimes.UnitTime.Year:
                    var startOfYear = new DateTime(myDate.Year, 1, 1);
                    var endOfYear = new DateTime(myDate.Year, 12, 31);
                    expenditures = expenditures.Where(x => x.DateCreate >= startOfYear && x.DateCreate <= endOfYear);
                    break;
                default:
                    expenditures = expenditures.Where(x => x.DateCreate.Date == DateTime.Now.Date);
                    break;
            }
            var sumSpend = expenditures.Sum(x => x.Cost);
            var budgetResponse = new BudgetResponse()
            {
                Id = budget.Id,
                LimitMoney   = budget.LimitMoney.ToString(),
                UnitTime = budget.UnitTime,
                NameUnitTime = UnitTimeGenerator.GenerateName(budget.UnitTime),
                SpendMoney = sumSpend.ToString(),
                RemainMoney = (budget.LimitMoney - sumSpend).ToString()
            };
            return new ApiSuccessResult<BudgetResponse>(resultObj: budgetResponse);
        }

        public ApiResultBase<BudgetResponse> UpdateBudgetOfUser(BudgetUpdateRequest request, Guid userId)
        {
            var budget = _budgetRepository.GetBudget(userId);
            if(budget is null)
            {
                return new ApiErrorResult<BudgetResponse>(message: "Cập nhập không thành công!");
            }    
            budget.UnitTime = request.UnitTime;
            budget.LimitMoney = request.LimitMoney;
            _budgetRepository.UpdateBudget(budget);
            var response = GetBudgetOfUser(userId);
            response.Message = "Cập nhật thành công";
            return response;
        }
    }
}
