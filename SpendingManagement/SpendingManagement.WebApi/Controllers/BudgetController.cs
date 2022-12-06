using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpendingManagement.Application.Budgets;

namespace SpendingManagement.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BudgetController : ControllerBase
    {
        private readonly IBudgetService _budgetService;
        public BudgetController(IBudgetService budgetService)
        {
            _budgetService = budgetService;
        }

        [HttpGet("{id}")]
        public IActionResult GetBudgetOfUser(Guid id)//id = userId
        {
            try
            {
                return Ok(_budgetService.GetBudgetOfUser(id));
            }
            catch (Exception)
            {
                throw new Exception("Some thing has gone wrong!");
            }
        }

        [HttpPut("update/{id}")]//idUser
        public IActionResult UpdateBudgetOfUser(Guid id, BudgetUpdateRequest request)
        {
            try
            {
                return Ok(_budgetService.UpdateBudgetOfUser(request, id));
            }
            catch (Exception)
            {
                throw new Exception("Some thing has gone wrong!");
            }
        }
    }
}
