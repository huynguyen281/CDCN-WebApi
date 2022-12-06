using SpendingManagement.Data.Entities;
using SpendingManagement.Share.ApiResults;
using SpendingManagement.Share.TypeOfCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpendingManagement.Application.Categories
{
    public interface ICategoryService
    {
        ApiResultBase<List<CategoryResponse>> GetAllCategory();
        ApiResultBase<List<CategoryResponse>> GetCategoriesByCategoryType(CategoryType categoryType);

    }
}
