using Microsoft.EntityFrameworkCore;
using SpendingManagement.Repository.Expenditures;
using SpendingManagement.Share.ApiResults;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SpendingManagement.Application.Statistics
{
    public class StatisticService : IStatisticService
    {
        private readonly IExpenditureRepository _expenditureRepository;
        public StatisticService(IExpenditureRepository expenditureRepository)
        {
            _expenditureRepository = expenditureRepository;
        }

        public async Task<ApiResultBase<List<StatisticResponse>>> GetStatisticByMonth(DateTime startDate, DateTime endDate, Guid userId)
        {
            var results = await _expenditureRepository.GetAllExpenditures()
                .Where(x => x.AppUserId == userId && (x.DateCreate >= startDate && x.DateCreate <= endDate))
                .Include(x => x.Category)
                .OrderBy(x => x.DateCreate)
                .GroupBy(o => new { Date = o.DateCreate.Date })
                .Select(x => new StatisticResponse()
                {
                    Time = x.Key.Date.ToString("dd-MM-yyyy"),
                    ReceiveMoney = x.Where(x => x.Category.TypeOfCategory == Share.TypeOfCategory.CategoryType.In).Sum(x => x.Cost).ToString(),
                    SpendMoney = x.Where(x => x.Category.TypeOfCategory == Share.TypeOfCategory.CategoryType.Out).Sum(x => x.Cost).ToString(),
                }).ToListAsync();
            foreach(DateTime date in EachDay(startDate, endDate))
            {
                if(results.Where(x => x.Time.Equals(date.ToString("dd-MM-yyyy"))).Count() == 0)
                {
                    results.Add(new StatisticResponse()
                    {
                        Time = date.ToString("dd-MM-yyyy"),
                        ReceiveMoney = "0",
                        SpendMoney = "0"
                    });
                }    
            }
            results = results.OrderBy(x => DateTime.ParseExact(x.Time, "dd-MM-yyyy", CultureInfo.GetCultureInfo("tr-TR"))).ToList();
            return new ApiSuccessResult<List<StatisticResponse>>(resultObj: results);
        }

        public async Task<ApiResultBase<List<StatisticResponse>>> GetStatisticByYear(int year, Guid userId)
        {
            var results = await _expenditureRepository.GetAllExpenditures()
                .Where(x => x.AppUserId == userId && x.DateCreate.Year == year)
                .Include(x => x.Category)
                .OrderBy(x => x.DateCreate)
                .GroupBy(o => new { Month = o.DateCreate.Month })
                .Select(x => new StatisticResponse()
                {
                    Time = x.Key.Month.ToString(),
                    ReceiveMoney = x.Where(x => x.Category.TypeOfCategory == Share.TypeOfCategory.CategoryType.In).Sum(x => x.Cost).ToString(),
                    SpendMoney = x.Where(x => x.Category.TypeOfCategory == Share.TypeOfCategory.CategoryType.Out).Sum(x => x.Cost).ToString(),
                }).ToListAsync();
            if(results.Count() < 12)
            {
                for(int i = 1; i <= 12; i++)
                {
                    if(results.Where(x => x.Time.Equals(i.ToString())).Count() == 0)
                    {
                        results.Add(new StatisticResponse()
                        {
                            Time = i.ToString(),
                            ReceiveMoney = "0",
                            SpendMoney = "0"
                        }); ;
                    }
                }    
            }
            results = results.OrderBy(x => int.Parse(x.Time)).ToList();
            return new ApiSuccessResult<List<StatisticResponse>>(resultObj: results);
        }
        private IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
        {
            for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
                yield return day;
        }
    }
}
