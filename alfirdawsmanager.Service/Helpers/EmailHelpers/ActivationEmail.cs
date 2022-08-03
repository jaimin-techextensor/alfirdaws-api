using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace alfirdawsmanager.Service.Helpers.EmailHelpers
{
    public class ActivationEmail
    {
        static IHostingEnvironment _hostingEnvironment;
        static string EmailTemplate;


        public static void Initialize(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public static bool SendActivationEmail(string _sendMailTo,string activationUrl)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("bhavik.thakkar@techextensor.com", "DXN3FmgtnK518UhJ");
                mail.To.Add(_sendMailTo);
                mail.Subject = "Email Verification";
                if (EmailTemplate == null)
                    EmailTemplate = ReadPhysicalFile("Templates/ActivationEmailTemplate.html");
                string emailMessage = EmailTemplate.Replace("{URL}", activationUrl);
                mail.Body = emailMessage;
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp-relay.sendinblue.com";
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential("bhavik.thakkar@techextensor.com", "DXN3FmgtnK518UhJ");
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.Send(mail);
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private static string ReadPhysicalFile(string path)
        {
            if (_hostingEnvironment == null)
                throw new InvalidOperationException($"{nameof(ActivationEmail)} is not initialized");

            IFileInfo fileInfo = _hostingEnvironment.ContentRootFileProvider.GetFileInfo(path);

            if (!fileInfo.Exists)
                throw new FileNotFoundException($"Template file located at \"{path}\" was not found");

            using (var fs = fileInfo.CreateReadStream())
            {
                using (var sr = new StreamReader(fs))
                {
                    return sr.ReadToEnd();
                }
            }
        }

    }
}
