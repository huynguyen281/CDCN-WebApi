using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpendingManagement.Application.Statistics;
using System.Globalization;

namespace SpendingManagement.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticController : ControllerBase
    {
        private readonly IStatisticService _statisticService;
        public StatisticController(IStatisticService statisticService)
        {
            _statisticService = statisticService;
        }
        [HttpGet("month")]
        public async Task<IActionResult> GetStatisticByMonth(string startDate, string endDate, Guid id)
        {
            try
            {
                var start = DateTime.ParseExact(startDate, "dd-MM-yyyy", CultureInfo.GetCultureInfo("tr-TR"));
                var end = DateTime.ParseExact(endDate, "dd-MM-yyyy", CultureInfo.GetCultureInfo("tr-TR"));
                return Ok(await _statisticService.GetStatisticByMonth(start, end, id));
            }
            catch (Exception)
            {
                throw new Exception("Some thing has gone wrong!");
            }
        }

        [HttpGet("year")]
        public async Task<IActionResult> GetStatisticByYear(int year, Guid id)
        {
            try
            {
                return Ok(await _statisticService.GetStatisticByYear(year, id));
            }
            catch (Exception)
            {
                throw new Exception("Some thing has gone wrong!");
            }
        }
    }
}
