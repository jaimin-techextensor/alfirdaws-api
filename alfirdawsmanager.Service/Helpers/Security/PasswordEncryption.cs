using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace alfirdawsmanager.Service.Helpers.Security
{
    public class PasswordEncryption
    {
        private static object encryptionKey = "ALFIRDAWSMANAGERSECRETKEY";

        public static string ToEncrypt(string password)
        {
            byte[] key = { };
            byte[] iv = { 10, 20, 30, 40, 50, 60, 70, 80 };
            byte[] inputByteArray;
            try
            {
                key = Encoding.UTF8.GetBytes(encryptionKey.ToString().Substring(0, 8));
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
    }
}
