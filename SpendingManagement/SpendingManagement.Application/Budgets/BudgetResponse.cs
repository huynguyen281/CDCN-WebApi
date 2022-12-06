using SpendingManagement.Share.UnitTimes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpendingManagement.Application.Budgets
{
    public class BudgetResponse
    {
        public Guid Id { get; set; }
        public string? LimitMoney { get; set; }
        public string? SpendMoney { get; set; }
        public string? RemainMoney { get; set; }
        public  UnitTime UnitTime { get; set; }
        public string? NameUnitTime { get; set; }
    }
}
