using Moq;
using SpendingManagement.Application.Statistics;
using SpendingManagement.Data.Entities;
using SpendingManagement.Repository.Expenditures;

namespace SpendingManagement.NTest.ApplicationTests
{
    public class StatisticServiceTest
    {
        private Mock<IExpenditureRepository> _expenditureIRepositoryMock;
        private List<Expenditure> _availableListExpenditures;
        private Expenditure _availableExpenditure;
        private StatisticService _statisticServiceService;

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
                Category = new Category()
                {
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

            _expenditureIRepositoryMock = new Mock<IExpenditureRepository>();
            _expenditureIRepositoryMock.Setup(x => x.GetAllExpenditures()).Returns(_availableListExpenditures.AsQueryable());
            _expenditureIRepositoryMock.Setup(x => x.GetExpenditureById(_availableExpenditure.Id)).Returns(_availableExpenditure);

            _statisticServiceService = new StatisticService(_expenditureIRepositoryMock.Object);
        }

        [Test]
        public void GetStatisticByMonth_ReturnApiSuccessWithStatisticByMonth()
        {
            var result = _statisticServiceService.GetStatisticByMonth(DateTime.Now, DateTime.Now, _availableExpenditure.AppUserId);
            //assert
            Assert.IsNotNull(result);
            Assert.That(true, Is.EqualTo(result.IsCompleted));
        }

        [Test]
        public void GetStatisticByYear_ReturnApiSuccessWithStatisticByYear()
        {
            var result = _statisticServiceService.GetStatisticByYear(DateTime.Now.Year, _availableExpenditure.AppUserId);
            //assert
            Assert.IsNotNull(result);
            Assert.That(true, Is.EqualTo(result.IsCompleted));
        }
    }
}
