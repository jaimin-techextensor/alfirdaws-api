using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using alfirdawsmanager.Data.Models;

namespace alfirdawsmanager.Service.Interface
{
    public interface IAuthenticateInterface
    {
        Task<User> AuthenticateUser(string UserName, string Password);
    }
}
