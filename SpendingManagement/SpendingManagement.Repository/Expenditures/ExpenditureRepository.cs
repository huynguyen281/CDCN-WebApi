using DbFactory;
using SpendingManagement.Data.EF;
using SpendingManagement.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpendingManagement.Repository.Expenditures
{
    public class ExpenditureRepository : GenericRepository<Expenditure, Guid, ApplicationDbContext>, IExpenditureRepository
    {
        public ExpenditureRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public IQueryable<Expenditure> GetAllExpenditures()
        {
            return FindAll();
        }
        public void AddExpenditure(Expenditure expenditure)
        {
            Add(expenditure);
            DbContext.SaveChanges();
        }

        public void UpdateExpenditure(Expenditure expenditure)
        {
            Update(expenditure);
            DbContext.SaveChanges();

        }

        public Expenditure? GetExpenditureById(Guid id)
        {
            return FindById(id);
        }

        public void DeleteExpenditure(Guid id)
        {
            Remove(id);
            DbContext.SaveChanges();
        }
    }
}
