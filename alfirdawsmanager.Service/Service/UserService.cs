using alfirdawsmanager.Data.Models;
using alfirdawsmanager.Data.Repository;
using alfirdawsmanager.Service.Helpers.Security;
using alfirdawsmanager.Service.Interface;
using alfirdawsmanager.Service.Models;
using alfirdawsmanager.Service.Models.RequestModels;
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
        public PagedList<User> GetUsersOverview(PageParamsRequestModel pageParamsRequestModel)
        {
            try
            {
                using (var repo = new RepositoryPattern<User>())
                {
                    var dataToReturn = PagedList<User>.ToPagedList(repo.SelectAll().OrderByDescending(a => a.UserId).AsQueryable(),
                    pageParamsRequestModel.PageNumber,
                    pageParamsRequestModel.PageSize);

                    return dataToReturn;
                }
            }
            catch (Exception)
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

                    //foreach (var item in dataToReturn)
                    //{
                    //    if (item.Picture != null)
                    //    {
                    //        //item.Picture = GetImage(Convert.ToBase64String(item.Picture));
                    //    }
                    //}
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
                        if (dataToReturn.Password != null)
                        {
                             dataToReturn.Password = PasswordEncryption.DecodeFrom64(dataToReturn.Password);
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
               // string byteImage = UploadFile(userModel);
                var objUserModel = new User();
                objUserModel.UserName = userModel.UserName;
                objUserModel.Password = PasswordEncryption.EncodePasswordToBase64(userModel.Password);
                objUserModel.Name = userModel.Name;
                objUserModel.LastName = userModel.LastName;
                objUserModel.Picture = userModel.Picture;
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
               // string byteImage = UploadFile(userModel);
                var obj = _context.Users.Where(a => a.UserId == userModel.UserId).SingleOrDefault();
                if (obj != null)
                {
                    obj.UserName = userModel.UserName;
                    obj.Password = PasswordEncryption.EncodePasswordToBase64(userModel.Password);
                    obj.Name = userModel.Name;
                    obj.LastName = userModel.LastName;
                    obj.Picture = userModel.Picture;
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
        //private string UploadFile(UserModel userModel)
        //{
        //    string base64String = null;
        //    if (userModel.Picture != null)
        //    {
        //        using (var ms = new MemoryStream())
        //        {
        //            userModel.Picture.CopyTo(ms);
        //            var fileBytes = ms.ToArray();
        //            base64String = Convert.ToBase64String(fileBytes);
        //        }
        //    }
        //    return base64String;
        //}

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
