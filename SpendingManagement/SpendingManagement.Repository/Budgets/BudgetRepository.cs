using DbFactory;
using Microsoft.EntityFrameworkCore;
using SpendingManagement.Data.EF;
using SpendingManagement.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpendingManagement.Repository.Budgets
{
    public class BudgetRepository : GenericRepository<Budget, Guid, ApplicationDbContext>, IBudgetRepository
    {
        private readonly ApplicationDbContext _dbcontext;
        public BudgetRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbcontext = dbContext;
        }

        public void Create(Budget budget)
        {
            Add(budget);
            DbContext.SaveChanges();
        }

        public Budget? GetBudget(Guid userId)
        {
            Budget? budget = FindAll().Where(x => x.AppUserId == userId).FirstOrDefault();
            return budget;
        }

        public int UpdateBudget(Budget budget)
        {
            Update(budget);
            return DbContext.SaveChanges();
        }
    }
}
