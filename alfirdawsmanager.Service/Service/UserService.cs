using alfirdawsmanager.Data.Models;
using alfirdawsmanager.Data.Repository;
using alfirdawsmanager.Service.Helpers.Security;
using alfirdawsmanager.Service.Interface;
using alfirdawsmanager.Service.Models;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alfirdawsmanager.Service.Service
{
    public class UserService : IUserInterface
    {
        #region Members

        private readonly AlfirdawsManagerDbContext _context;
        static IHostingEnvironment _hostingEnvironment;
        private readonly IMapper _mapper;

        #endregion

        #region Constructors

        public UserService(AlfirdawsManagerDbContext context, IMapper mapper, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _mapper = mapper;
            _hostingEnvironment = hostingEnvironment;
        }

        #endregion

        #region Methods

        /// <summary>
        /// GetUsersOverview
        /// </summary>
        /// <returns></returns>
        public async Task<List<User>> GetUsersOverview()
        {
            try
            {
                var dataToReturn = new List<User>();
                using (var repo = new RepositoryPattern<User>())
                {
                    dataToReturn = _mapper.Map<List<User>>(repo.SelectAll().OrderByDescending(a => a.UserId).ToList());

                    foreach (var item in dataToReturn)
                    {
                        if (item.Picture != null)
                        {
                            item.Picture = GetImage(Convert.ToBase64String(item.Picture));
                        }
                    }
                }
                return dataToReturn;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// SearchUsers
        /// </summary>
        /// <returns></returns>
        public async Task<List<User>> SearchUsers(string searchText)
        {
            try
            {
                var dataToReturn = new List<User>();
                using (var repo = new RepositoryPattern<User>())
                {
                    dataToReturn = _mapper.Map<List<User>>(repo.SelectAll().OrderByDescending(a => a.UserId)
                                                                .Where(a =>
                                                                           ((a.UserName != null) && (a.UserName.Contains(searchText)))
                                                                        || ((a.Name != null) && (a.Name.Contains(searchText)))
                                                                        || ((a.LastName != null) && (a.LastName.Contains(searchText)))
                                                                        || ((a.Email != null) && (a.Email.Contains(searchText)))
                                                                       ).ToList());

                    foreach (var item in dataToReturn)
                    {
                        if (item.Picture != null)
                        {
                            item.Picture = GetImage(Convert.ToBase64String(item.Picture));
                        }
                    }
                }
                return dataToReturn;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// GetUserById
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public async Task<User> GetUserById(int UserId)
        {
            try
            {
                var dataToReturn = new User();
                using (var repo = new RepositoryPattern<User>())
                {
                    dataToReturn = _mapper.Map<User>(repo.SelectByID(UserId));
                    if (dataToReturn != null)
                    {
                        if (dataToReturn.Picture != null)
                        {
                            dataToReturn.Picture = GetImage(Convert.ToBase64String(dataToReturn.Picture));
                        }
                    }
                }
                return dataToReturn;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// CreateUser
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        public bool CreateUser(UserModel userModel)
        {
            try
            {
                bool success = true;
                byte[] uniqueFileName = UploadFile(userModel);
                var objUserModel = new User();
                //var objUserModel = _mapper.Map<User>(userModel);
                objUserModel.UserName = userModel.UserName;
                objUserModel.Password = PasswordEncryption.ToEncrypt(userModel.Password);
                objUserModel.Name = userModel.Name;
                objUserModel.LastName = userModel.LastName;
                objUserModel.Picture = uniqueFileName;
                objUserModel.Email = userModel.Email;
                objUserModel.Active = userModel.Active;
                objUserModel.LastLoginTime = DateTime.Now;
                objUserModel.SendActivationEmail = userModel.SendActivationEmail;
                objUserModel.ChangePwdAtNextLogin = userModel.ChangePwdAtNextLogin;

                using (var repo = new RepositoryPattern<User>())
                {
                    repo.Insert(objUserModel);
                    repo.Save();
                    success = true;
                }
                return success;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// UpdateUser
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        public bool UpdateUser(UserModel userModel)
        {
            try
            {
                bool success = true;
                byte[] uniqueFileName = UploadFile(userModel);
                var obj = _context.Users.Where(a => a.UserId == userModel.UserId).SingleOrDefault();
                if (obj != null)
                {
                    obj.UserName = userModel.UserName;
                    obj.Password = PasswordEncryption.ToEncrypt(userModel.Password);
                    obj.Name = userModel.Name;
                    obj.LastName = userModel.LastName;
                    obj.Picture = uniqueFileName;
                    obj.Email = userModel.Email;
                    obj.Active = userModel.Active;
                    obj.LastLoginTime = DateTime.Now;
                    obj.SendActivationEmail = userModel.SendActivationEmail;
                    obj.ChangePwdAtNextLogin = userModel.ChangePwdAtNextLogin;
                }
                using (var repo = new RepositoryPattern<User>())
                {
                    repo.Update(obj);
                    repo.Save();
                    success = true;
                }
                return success;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// DeleteUser
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public bool DeleteUser(int UserId)
        {
            try
            {
                bool success = true;
                using (var repo = new RepositoryPattern<User>())
                {
                    var res= _mapper.Map<User>(repo.SelectByID(UserId));
                    if (res != null) 
                    { 
                        repo.Delete(UserId);
                        repo.Save();
                        success = true;
                        return success;
                    }
                    return success = false;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Image to Byte
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        private byte[] UploadFile(UserModel userModel)
        {
            //string uniqueFileName1 = null;
            string base64String = null;

            if (userModel.Picture != null)
            {
                MemoryStream ms = new MemoryStream();
                userModel.Picture.CopyTo(ms);
                byte[] imageBytes = ms.ToArray();
                base64String = Convert.ToBase64String(imageBytes);
                //uniqueFileName = ms.ToArray().ToString();
                //string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "ProfilePictures");
                //uniqueFileName = Guid.NewGuid().ToString() + "_" + userModel.Picture.FileName;
                //string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                //using (var fileStream = new FileStream(filePath, FileMode.Create))
                //{
                //    userModel.Picture.CopyTo(fileStream);
                //}
            }
            return Encoding.UTF8.GetBytes(base64String);
        }

        /// <summary>
        /// Byte to Image
        /// </summary>
        /// <param name="sBase64String"></param>
        /// <returns></returns>
        public byte[] GetImage(string sBase64String)
        {
            byte[] bytes = null;
            if (!string.IsNullOrEmpty(sBase64String))
            {
                bytes = Convert.FromBase64String(sBase64String);
            }
            return bytes;
        }

        #endregion
    }
}
