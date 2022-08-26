using alfirdawsmanager.Data.Models;
using alfirdawsmanager.Data.Repository;
using alfirdawsmanager.Service.Interface;
using alfirdawsmanager.Service.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using alfirdawsmanager.Service.Helpers.EmailHelpers;
using alfirdawsmanager.Service.Helpers.Security;

namespace alfirdawsmanager.Service.Service
{
    public class AuthenticateService : IAuthenticateInterface
    {
        private readonly AlfirdawsManagerDbContext _context;
        private static object encryptionKey = "ALFIRDAWSMANAGERSECRETKEY";
        public AuthenticateService(AlfirdawsManagerDbContext context)
        {
            _context = context;
        }


        #region Methods

        /// <summary>
        /// Authenticate
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public async Task<User> AuthenticateUser(string UserName, string Password)
        {
            try
            {
                var dataToReturn = new User();
                dataToReturn = _context.Users.SingleOrDefault(x => x.Email.Trim().ToLower() == UserName.Trim().ToLower());
                if (dataToReturn == null)
                {
                    return null;
                }
                if (dataToReturn.Password ==PasswordEncryption.EncodePasswordToBase64(Password))
                {
                    dataToReturn.LastLoginTime = DateTime.Now;
                    using (var repo = new RepositoryPattern<User>())
                    {
                        repo.Update(dataToReturn);
                        repo.Save();
                    }
                    return dataToReturn;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// ForgotPassword
        /// </summary>
        /// <param name="Email"></param>
        /// <returns></returns>
        public async Task<User> ForgotPassword(string Email)
        {
            try
            {
                var dataToReturn = new User();
                dataToReturn = _context.Users.SingleOrDefault(x => x.Email.Trim().ToLower() == Email.Trim().ToLower());
                if (dataToReturn == null)
                {
                    return null;
                }
                else
                {
                    string token = Guid.NewGuid().ToString();
                    string activationUrl = "http://techext-001-site50.itempurl.com/reset-password?email=" + dataToReturn.Email+"&token="+ token;
                    ActivationEmail.SendActivationEmail(Email, activationUrl);

                    dataToReturn.SendActivationEmail = true;
                    using (var repo = new RepositoryPattern<User>())
                    {
                        repo.Update(dataToReturn);
                        repo.Save();
                    }
;                    return dataToReturn;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// ResetPassword
        /// </summary>
        /// <param name="Email"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public async Task<User> ResetPassword(string Email,string Password)
        {
            try
            {
                var dataToReturn = new User();
                dataToReturn = _context.Users.SingleOrDefault(x => x.Email.Trim().ToLower() == Email.Trim().ToLower());
                if (dataToReturn == null)
                {
                    return null;
                }
                else
                {
                    dataToReturn.Password = PasswordEncryption.ToEncrypt(Password);
                    dataToReturn.IsPasswordChanged = true;
                    using (var repo = new RepositoryPattern<User>())
                    {
                        repo.Update(dataToReturn);
                        repo.Save();
                    }
                    return dataToReturn;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion
    }
}
