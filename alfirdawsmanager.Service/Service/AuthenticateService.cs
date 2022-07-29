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
                if (dataToReturn.Password ==ToEncrypt(Password))
                {
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

        private static string ToEncrypt(string password)
        {
            byte[] key = { };
            byte[] iv = { 10, 20, 30, 40, 50, 60, 70, 80 };
            byte[] inputByteArray;
            try
            {
                key=Encoding.UTF8.GetBytes(encryptionKey.ToString().Substring(0,8));
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                inputByteArray = Encoding.UTF8.GetBytes(password);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(key, iv), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                return Convert.ToBase64String(ms.ToArray());
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
    }
}
