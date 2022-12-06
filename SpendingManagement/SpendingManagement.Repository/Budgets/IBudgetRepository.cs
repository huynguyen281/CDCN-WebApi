using SpendingManagement.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SpendingManagement.Repository.Budgets
{
    public interface IBudgetRepository
    {
        void Create(Budget budget);

        Budget? GetBudget(Guid userId);
        int UpdateBudget(Budget budget);
    }
}
