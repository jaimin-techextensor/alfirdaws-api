using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alfirdawsmanager.Service.Models.RequestModels
{
    public class ResetPasswordRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
