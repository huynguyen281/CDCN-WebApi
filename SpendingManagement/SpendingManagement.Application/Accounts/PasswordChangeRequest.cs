using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpendingManagement.Application.Accounts
{
    public class PasswordChangeRequest
    {
        [MinLength(8)]
        public string Password { get; set; }
    }
}
