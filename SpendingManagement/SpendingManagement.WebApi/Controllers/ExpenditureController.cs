using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpendingManagement.Application.Expenditures;
using System.Globalization;
using System.Net;

namespace SpendingManagement.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenditureController : ControllerBase
    {
        private readonly IExpenditureService _expenditureService;
        public ExpenditureController(IExpenditureService expenditureService)
        {
            _expenditureService = expenditureService;
        }

        [HttpGet()]
        public async Task<IActionResult> GetExpenditureOnDate(string date, Guid id)
        {
            try
            {
                DateTime? myDate = null;
                if(String.IsNullOrEmpty(date))
                {
                    myDate = DateTime.Now;
                }
                else
                {
                    myDate = DateTime.ParseExact(date, "dd-MM-yyyy", CultureInfo.GetCultureInfo("tr-TR"));
                }
                return Ok(await _expenditureService.GetExpendituresOnDate(myDate,id));
            }
            catch (Exception)
            {
                throw new Exception("Some thing has gone wrong!");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddExpenditure(CreateExpenditureRequest request)
        {
            try
            {
                return Ok(await _expenditureService.CreateExpenditure(request));
            }
            catch (Exception)
            {
                throw new Exception("Some thing has gone wrong!");
            }
        }
        
        [HttpPut("{id}")] 
        public async Task<IActionResult> UpdateExpenditure(Guid id, UpdateExpenditureRequest request)
        {
            try
            {
                if (id != request.Id)
                {
                    return new BadRequestResult();
                }
                return Ok(await _expenditureService.UpdateExpenditure(request));
            }
            catch (Exception)
            {
                throw new Exception("Some thing has gone wrong!");
            }
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExpenditure(Guid id)
        {
            try
            {
                return Ok(await _expenditureService.DeleteExpenditure(id));
            }
            catch (Exception)
            {
                throw new Exception("Some thing has gone wrong!");
            }
        }
    }
}
