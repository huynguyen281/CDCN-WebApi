using Moq;
using SpendingManagement.Application.Budgets;
using SpendingManagement.Application.Expenditures;
using SpendingManagement.Data.Entities;
using SpendingManagement.Repository.Budgets;
using SpendingManagement.Repository.Expenditures;
using SpendingManagement.Share.NameTypeOfCategoryGenerator;
using SpendingManagement.Share.UnitTimes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpendingManagement.NTest.ApplicationTests
{
    [TestFixture]
    public class ExpenditureServiceTest
    {
        private Mock<IExpenditureRepository> _expenditureIRepositoryMock;
        private Mock<ExpenditureRepository> _expenditureRepositoryMock;
        private List<Expenditure> _availableListExpenditures;
        private Expenditure _availableExpenditure;
        private CreateExpenditureRequest _createExpenditureRequest;
        private UpdateExpenditureRequest _updateExpenditureRequest;
        private ExpenditureService _expenditureService;

        [SetUp]
        public void SetUp()
        {
            var categoryId = Guid.NewGuid();
            _availableExpenditure = new Expenditure()
            {
                Id = Guid.NewGuid(),
                CategoryId = categoryId,
                DateCreate = DateTime.Now,
                AppUserId = Guid.NewGuid(),
                Note = "note",
                Cost = 150000,
                Category = new Category() {
                    Id = categoryId,
                    Name = "nameCategory",
                    TypeOfCategory = Share.TypeOfCategory.CategoryType.Out,
                    LinkIcon = "linkIcon",
                },
            };

            _availableListExpenditures = new List<Expenditure>()
            {
                _availableExpenditure
            };

            _createExpenditureRequest = new CreateExpenditureRequest()
            {
                UserId = Guid.NewGuid(),
                CategoryId = Guid.NewGuid(),
                Cost = 100000,
                Note = "note",
                CreationDate = DateTime.Now.ToString("dd-MM-yyyy")
            };

            _updateExpenditureRequest = new UpdateExpenditureRequest()
            {
                Id = _availableExpenditure.Id,
                CategoryId = Guid.NewGuid(),
                DateCreation = DateTime.Now.ToString("dd-MM-yyyy"),
                Note = "note",
                Cost = 150000
            };

            _expenditureIRepositoryMock = new Mock<IExpenditureRepository>();
            _expenditureIRepositoryMock.Setup(x => x.GetAllExpenditures()).Returns(_availableListExpenditures.AsQueryable());
            _expenditureIRepositoryMock.Setup(x => x.GetExpenditureById(_availableExpenditure.Id)).Returns(_availableExpenditure);

            _expenditureService = new ExpenditureService(_expenditureIRepositoryMock.Object);
        }

        [Test]
        public void CreateExpenditure_ReturnApiSuccessWithCreateExpenditure()
        {
            var result = _expenditureService.CreateExpenditure(_createExpenditureRequest);
            //assert
            Assert.IsNotNull(result);
            Assert.That(false, Is.EqualTo(result.IsFaulted));
            Assert.That(true, Is.EqualTo(result.IsCompleted));
        }

        [Test]
        public void UpdateExpenditure_ReturnApiSuccessWithUpdateExpenditure()
        {
            var result = _expenditureService.UpdateExpenditure(_updateExpenditureRequest);
            //assert
            Assert.IsNotNull(result);
            Assert.That(false, Is.EqualTo(result.IsFaulted));
            Assert.That(true, Is.EqualTo(result.IsCompleted));
        }

        [Test]
        public void DeleteExpenditure_ReturnApiSuccessWithDeleteExpenditure()
        {
            var result = _expenditureService.DeleteExpenditure(_availableExpenditure.Id);
            //assert
            Assert.IsNotNull(result);
            Assert.That(false, Is.EqualTo(result.IsFaulted));
            Assert.That(true, Is.EqualTo(result.IsCompleted));
        }

        [Test]
        public void GetAllExpenditures_OnDate_ReturnApiSuccessWithGetExpendituresOnDate()
        {
            var result = _expenditureService.GetExpendituresOnDate(DateTime.Now, _availableExpenditure.AppUserId);
            //assert
            Assert.IsNotNull(result);
            Assert.That(false, Is.EqualTo(result.IsFaulted));
            Assert.That(true, Is.EqualTo(result.IsCompleted));
            Assert.That(_availableListExpenditures.Count, Is.EqualTo(result.Result.ResultObj.Count));
            foreach (var item in result.Result.ResultObj)
            {
                Assert.That(_availableExpenditure.Id, Is.EqualTo(item.Id));
                Assert.That(_availableExpenditure.Cost.ToString(), Is.EqualTo(item.Cost));
                Assert.That(_availableExpenditure.DateCreate.ToString("dd-MM-yyyy"), Is.EqualTo(item.Date));
                Assert.That(_availableExpenditure.Note, Is.EqualTo(item.Note));
                Assert.That(_availableExpenditure.CategoryId, Is.EqualTo(item.CategoryId));
                Assert.That(_availableExpenditure.Category.TypeOfCategory, Is.EqualTo(item.CategoryType));
                Assert.That(_availableExpenditure.Category.Name, Is.EqualTo(item.CategoryName));
                Assert.That(_availableExpenditure.Category.LinkIcon, Is.EqualTo(item.ImageIcon));
                Assert.That(TypeCategoryNameGenerator.GenerateName(_availableExpenditure.Category.TypeOfCategory), Is.EqualTo(item.CategoryTypeName));

            }
        }
    }
}
