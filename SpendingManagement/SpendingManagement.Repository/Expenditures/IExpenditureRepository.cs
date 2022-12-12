using SpendingManagement.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpendingManagement.Repository.Expenditures
{
    public interface IExpenditureRepository
    {
        IQueryable<Expenditure> GetAllExpenditures();
        Task AddExpenditure(Expenditure expenditure);
        Task UpdateExpenditure(Expenditure expenditure);

        Expenditure? GetExpenditureById(Guid id);
        Task DeleteExpenditure(Guid id);
    }
}
