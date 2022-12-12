using SpendingManagement.Share.Gender;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpendingManagement.Application.Accounts
{
    public class UpdateInfoAccountRequest
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
