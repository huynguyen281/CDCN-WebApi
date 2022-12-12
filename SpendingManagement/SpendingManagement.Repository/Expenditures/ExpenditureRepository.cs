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
        public async Task AddExpenditure(Expenditure expenditure)
        {
            Add(expenditure);
            await DbContext.SaveChangesAsync();
        }

        public async Task UpdateExpenditure(Expenditure expenditure)
        {
            Update(expenditure);
            await DbContext.SaveChangesAsync();

        }

        public Expenditure? GetExpenditureById(Guid id)
        {
            return FindById(id);
        }

        public async Task DeleteExpenditure(Guid id)
        {
            Remove(id);
            await DbContext.SaveChangesAsync();
        }
    }
}
