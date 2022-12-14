using Moq;
using SpendingManagement.Application.Budgets;
using SpendingManagement.Data.Entities;
using SpendingManagement.Repository.Budgets;
using SpendingManagement.Repository.Expenditures;
using SpendingManagement.Share.NameUnitTimeGenerator;
using SpendingManagement.Share.UnitTimes;

namespace SpendingManagement.NTest.ApplicationTests
{
    [TestFixture]
    public class BudgetServiceTest
    {
        private Mock<IBudgetRepository> _budgetRepositoryMock;
        private Mock<IExpenditureRepository> _expenditureRepositoryMock;
        private Budget _availablBudget1;
        private Budget _availablBudget2;
        private BudgetUpdateRequest _availablBudgetUpdateRequest;
        private IEnumerable<Expenditure> _availableListExpenditures;
        private BudgetService _budgetService;

        [SetUp]
        public void SetUp()
        {
            _availablBudget1 = new Budget()
            {
                Id = Guid.NewGuid(),
                AppUserId = Guid.NewGuid(),
                AppUser = new AppUser(),
                LimitMoney = 1000000,
                UnitTime = UnitTime.Day
            };
            _availablBudget2 = new Budget()
            {
                Id = Guid.NewGuid(),
                AppUserId = Guid.NewGuid(),
                AppUser = new AppUser(),
                LimitMoney = 1500000,
                UnitTime = UnitTime.Day
            };
            _availablBudgetUpdateRequest = new BudgetUpdateRequest()
            {
                UnitTime = UnitTime.Month,
                LimitMoney = 2000000
            };
            _availableListExpenditures = new List<Expenditure>()
            {
                new Expenditure()
                {
                    Category = new Category(){TypeOfCategory = Share.TypeOfCategory.CategoryType.Out},
                    DateCreate = DateTime.Now,
                    Cost = 100000
                }
            };

            _budgetRepositoryMock = new Mock<IBudgetRepository>();
            _budgetRepositoryMock.Setup(x => x.GetBudget(_availablBudget1.AppUserId)).Returns(_availablBudget1);
            _budgetRepositoryMock.Setup(x => x.GetBudget(_availablBudget2.AppUserId)).Returns(_availablBudget2);

            _expenditureRepositoryMock = new Mock<IExpenditureRepository>();
            _expenditureRepositoryMock.Setup(x => x.GetAllExpenditures()).Returns(_availableListExpenditures.AsQueryable());

            _budgetService = new BudgetService(_budgetRepositoryMock.Object, _expenditureRepositoryMock.Object);
        }

        [Test]
        public void GetBudgetOfUser_ReturnApiSuccessWithAllBudgetOfUser()
        {
            var result = _budgetService.GetBudgetOfUser(_availablBudget1.AppUserId);
            var sumSpend = _availableListExpenditures.Sum(x => x.Cost);
            //assert
            Assert.IsNotNull(result);
            Assert.That(true, Is.EqualTo(result.IsSuccessed));
            Assert.That(_availablBudget1.Id, Is.EqualTo(result.ResultObj.Id));
            Assert.That(_availablBudget1.UnitTime, Is.EqualTo(result.ResultObj.UnitTime));
            Assert.That(_availablBudget1.LimitMoney.ToString(), Is.EqualTo(result.ResultObj.LimitMoney));
            Assert.That(UnitTimeGenerator.GenerateName(_availablBudget1.UnitTime), Is.EqualTo(result.ResultObj.NameUnitTime));
            Assert.That(sumSpend.ToString(), Is.EqualTo(result.ResultObj.SpendMoney));
            Assert.That((_availablBudget1.LimitMoney - sumSpend).ToString(), Is.EqualTo(result.ResultObj.RemainMoney));
        }

        [Test]
        public void UpdateBudget_ReturnApiSuccessWithBudgetUpdate()
        {
            var result = _budgetService.UpdateBudgetOfUser(_availablBudgetUpdateRequest, _availablBudget1.AppUserId);
            //assert
            Assert.IsNotNull(result);
            Assert.That(true, Is.EqualTo(result.IsSuccessed));
            Assert.That(_availablBudgetUpdateRequest.UnitTime, Is.EqualTo(result.ResultObj.UnitTime));
            Assert.That(_availablBudgetUpdateRequest.LimitMoney.ToString(), Is.EqualTo(result.ResultObj.LimitMoney));
        }
    }
}
