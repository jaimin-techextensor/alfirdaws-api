using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alfirdawsmanager.Service.Models
{
    public class UserModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public byte[] Picture { get; set; }
        public string Email { get; set; }
        public bool Active { get; set; }
        public DateTime LastLoginTime { get; set; }
        public bool IsPasswordChanged { get; set; }
        public bool SendActivationEmail { get; set; }
        public bool ChangePwdAtNextLogin { get; set; }
    }
}
