using SpendingManagement.Share.ApiResults;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpendingManagement.Application.Expenditures
{
    public interface IExpenditureService
    {
        Task<ApiResultBase<List<ExpenditureResponse>>> GetExpendituresOnDate(DateTime? dateTime, Guid id);
        Task<ApiResultBase<string>> CreateExpenditure(CreateExpenditureRequest request);
        Task<ApiResultBase<string>> UpdateExpenditure(UpdateExpenditureRequest request);
        Task<ApiResultBase<string>> DeleteExpenditure(Guid id);
    }
}
