using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpendingManagement.Application.Expenditures
{
    public class UpdateExpenditureRequest
    {
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        public long Cost { get; set; }
        public string Note { get; set; }
        public string DateCreation { get; set; }
    }
}
