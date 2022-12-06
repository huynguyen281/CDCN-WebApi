using SpendingManagement.Share.UnitTimes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpendingManagement.Application.Budgets
{
    public class BudgetUpdateRequest
    {
        [Range(1, long.MaxValue)]
        public long LimitMoney { get; set; }
        public UnitTime UnitTime { get; set; }
    }
}
