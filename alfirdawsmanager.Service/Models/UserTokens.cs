using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alfirdawsmanager.Service.Models
{
    public class UserTokens
    {
        public string Token
        {
            get;
            set;
        }
        public string UserName
        {
            get;
            set;
        }
        public TimeSpan ExpiresIn
        {
            get;
            set;
        }
        //public string RefreshToken
        //{
        //    get;
        //    set;
        //}
        public int Id
        {
            get;
            set;
        }
        public string Email
        {
            get;
            set;
        }
        //public Guid GuidId
        //{
        //    get;
        //    set;
        //}
        //public DateTime ExpiredTime
        //{
        //    get;
        //    set;
        //}
    }
}
