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

namespace alfirdawsmanager.Service.Service
{
    public class UserService : IUserInterface
    {
        private readonly AlfirdawsManagerDbContext _context;
        private readonly IMapper _mapper;

        public UserService(AlfirdawsManagerDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        #region Methods

        /// <summary>
        /// GetUsersOverview
        /// </summary>
        /// <returns></returns>
        public async Task<List<UserModel>> GetUsersOverview()
        {
            try
            {
                var dataToReturn = new List<UserModel>();
                using (var repo = new RepositoryPattern<User>())
                {
                    dataToReturn = _mapper.Map<List<UserModel>>(repo.SelectAll().OrderByDescending(a => a.UserId).ToList());
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
        public async Task<List<UserModel>> SearchUsers(string searchText)
        {
            try
            {
                var dataToReturn = new List<UserModel>();
                using (var repo = new RepositoryPattern<User>())
                {
                    dataToReturn = _mapper.Map<List<UserModel>>(repo.SelectAll().OrderByDescending(a => a.UserId)
                                                                .Where(a=>
                                                                           ((a.UserName != null) && (a.UserName.Contains(searchText)))
                                                                        || ((a.Name !=null) && (a.Name.Contains(searchText))) 
                                                                        || ((a.LastName != null) && (a.LastName.Contains(searchText))) 
                                                                        || ((a.Email != null) && (a.Email.Contains(searchText)))
                                                                       ).ToList());
                }
                return dataToReturn;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion
    }
}
