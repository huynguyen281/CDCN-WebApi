using SpendingManagement.Share.Gender;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpendingManagement.Application.Accounts
{
    public class InfoAccountResponse
    {
        public Guid Id { get; set; } //idUser
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string DateOfBirth { get; set; }
        public GenderType Gender { get; set; }
        public string ImageURL { get; set; }
        public string Token { get; set; }
    }
}
