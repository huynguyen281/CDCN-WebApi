using SpendingManagement.Share.TypeOfCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpendingManagement.Application.Expenditures
{
    public class ExpenditureResponse
    {
        public Guid Id { get; set; }
        public string? CategoryName { get; set; }
        public Guid CategoryId { get; set; }
        public string? Cost { get; set; }
        public string? Date { get; set; }
        public CategoryType CategoryType{ get; set; }
        public string? CategoryTypeName { get; set; }
        public string? Note { get; set; }
        public string? ImageIcon { get; set; }
    }
}
