using Microsoft.EntityFrameworkCore.Query.Internal;
using Moq;
using SpendingManagement.Application.Categories;
using SpendingManagement.Data.Entities;
using SpendingManagement.Repository.Categories;
using SpendingManagement.Share.ApiResults;
using SpendingManagement.Share.TypeOfCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpendingManagement.NTest.ApplicationTests
{
    [TestFixture]
    public class CategoryServiceTest
    {
        private Mock<ICategoryRepository> _categoryRepositoryMock;
        private IEnumerable<Category> _availableListCategory;
        private CategoryService _categoryService;

        [SetUp]
        public void SetUp()
        {
            _availableListCategory = new List<Category>()
            {
                new Category()
                {
                    Id = new Guid("a76b02b9-4877-4c27-8133-9de232a6f58b"),
                    Description = "des1",
                    LinkIcon = "/link1",
                    Name = "Name1",
                    TypeOfCategory = Share.TypeOfCategory.CategoryType.In
                },
                new Category()
                {
                    Id = new Guid("536ef712-dd64-4c2d-9719-b7f2c248ed8d"),
                    Description = "des2",
                    LinkIcon = "/link2",
                    Name = "Name2",
                    TypeOfCategory = Share.TypeOfCategory.CategoryType.Out
                }
            };
            _categoryRepositoryMock = new Mock<ICategoryRepository>();
            _categoryRepositoryMock.Setup(x => x.GetAllCategory()).Returns(_availableListCategory.AsQueryable());
            _categoryService = new CategoryService(_categoryRepositoryMock.Object);
        }
        [Test]
        public void GetCateByType_TypeIn_ReturnApiSuccessWithAllCateTypeIn()
        {
            //arrange
            CategoryType type = CategoryType.In;
            //act
            var result = _categoryService.GetCategoriesByCategoryType(type);
            //assert
            Assert.IsNotNull(result);
            Assert.That(true, Is.EqualTo(result.IsSuccessed));
            Assert.That(1, Is.EqualTo(result.ResultObj.Count()));
        }

        [Test]
        public void GetAllCate_None_ReturnAllCate()
        {
            //act
            var result = _categoryService.GetAllCategory();
            //assert
            Assert.IsNotNull(result);
            Assert.That(true, Is.EqualTo(result.IsSuccessed));
            Assert.That(2, Is.EqualTo(result.ResultObj.Count()));
        }    
    }
}
