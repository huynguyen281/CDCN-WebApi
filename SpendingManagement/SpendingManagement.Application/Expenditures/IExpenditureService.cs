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
        ApiResultBase<List<ExpenditureResponse>> GetExpendituresOnDate(DateTime? dateTime, Guid id);
        ApiResultBase<string> CreateExpenditure(CreateExpenditureRequest request);
        ApiResultBase<string> UpdateExpenditure(UpdateExpenditureRequest request);
        ApiResultBase<string> DeleteExpenditure(Guid id);
    }
}
