﻿using Microsoft.EntityFrameworkCore;
using SpendingManagement.Repository.Categories;
using SpendingManagement.Share.ApiResults;
using SpendingManagement.Share.NameTypeOfCategoryGenerator;
using SpendingManagement.Share.TypeOfCategory;

namespace SpendingManagement.Application.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public ApiResultBase<List<CategoryResponse>> GetAllCategory()
        {
            var result = _categoryRepository.GetAllCategory().Select(x => new CategoryResponse()
            {
                Id = x.Id,
                CategoryType = x.TypeOfCategory,
                Description = x.Description,
                Name = x.Name,
                LinkIcon = x.LinkIcon,
                TypeOfCategoryName = TypeCategoryNameGenerator.GenerateName(x.TypeOfCategory)
            }).ToList();
            return new ApiSuccessResult<List<CategoryResponse>>(resultObj: result);
        }

        public ApiResultBase<List<CategoryResponse>> GetCategoriesByCategoryType(CategoryType categoryType)
        {
            var result = _categoryRepository.GetAllCategory().Where(x => x.TypeOfCategory.Equals(categoryType))
                .Select(x => new CategoryResponse()
                {
                    Id = x.Id,
                    CategoryType = x.TypeOfCategory,
                    Description = x.Description,
                    Name = x.Name,
                    LinkIcon = string.IsNullOrWhiteSpace(x.LinkIcon) ? "" : x.LinkIcon,
                    TypeOfCategoryName = TypeCategoryNameGenerator.GenerateName(x.TypeOfCategory)
                }).ToList();
            var itemDefault = result.Find(x => x.LinkIcon.Contains("default.png"));
            int numberItemRemoved = result.RemoveAll(x => x.LinkIcon.Contains("default.png"));
            result.Add(itemDefault);
            return new ApiSuccessResult<List<CategoryResponse>>(resultObj: result);

        }
    }
}