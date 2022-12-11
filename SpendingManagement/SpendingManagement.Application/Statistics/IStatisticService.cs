using SpendingManagement.Share.ApiResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpendingManagement.Application.Statistics
{
    public interface IStatisticService
    {
        Task<ApiResultBase<List<StatisticResponse>>> GetStatisticByMonth(DateTime startDate, DateTime endDate, Guid userId);
        Task<ApiResultBase<List<StatisticResponse>>> GetStatisticByYear(int year, Guid userId);
    }
}
