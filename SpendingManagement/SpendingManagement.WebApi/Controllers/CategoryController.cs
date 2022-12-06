using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpendingManagement.Application.Categories;
using SpendingManagement.Data.Entities;
using SpendingManagement.Share.TypeOfCategory;

namespace SpendingManagement.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("get-cate-all")]
        public IActionResult GetAllCategory()
        {
            try
            {
                var categoriesResponse =  _categoryService.GetAllCategory();
                return Ok(categoriesResponse);
            }
            catch (Exception)
            {
                throw new Exception("Some thing has gone wrong!");
            }
            
        }

        [HttpGet("get-cate-{type}")]
        public IActionResult GetCategoriesByCateType(CategoryType type)
        {
            try
            {
                var categoriesResponse = _categoryService.GetCategoriesByCategoryType(type);
                return Ok(categoriesResponse);
            }
            catch (Exception)
            {
                throw new Exception("Some thing has gone wrong!");
            }
        }
    }
}
