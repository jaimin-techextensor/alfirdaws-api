using System;
using alfirdawsmanager.Data.Models;
using alfirdawsmanager.Data.Repository;
using alfirdawsmanager.Service.Interface;
using alfirdawsmanager.Service.Models;
using alfirdawsmanager.Service.Models.RequestModels;
using AutoMapper;

namespace alfirdawsmanager.Service.Service
{
    public class CategoryService : ICategoryInterface
    {
        #region Members

        private readonly AlfirdawsManagerDbContext _context;
        private readonly IMapper _mapper;

        #endregion

        public CategoryService(AlfirdawsManagerDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        /// <summary>
        /// Retrives all the categories within the platform
        /// </summary>
        /// <returns>List of categories</returns>
        public Task<List<CategoryModel>> GetCategoriesOverview()
        {
            try
            {
                var dataToReturn = new List<CategoryModel>();

                using (var repo = new RepositoryPattern<Category>())
                {
                    List<Category> categories = repo.SelectAll().OrderBy(c => c.Sequence).ToList();

                    foreach (var category in categories)
                    {
                        CategoryModel catModel = new CategoryModel();
                        catModel.CategoryId = category.CategoryId;
                        catModel.Name = category.Name;
                        catModel.Sequence = category.Sequence;
                        catModel.Icon = category.Icon;
                        catModel.Active = category.Active;
                        catModel.CountSubcategories = category.SubCategories.Count();

                        dataToReturn.Add(catModel);
                    }
                    return Task.FromResult(dataToReturn);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Create a new category 
        /// </summary>
        /// <param name="catRequest">The category request</param>
        /// <returns>Boolean indication if the creation was successfull</returns>
        public bool CreateCategory(CategoryCreateRequest catRequest)
        {
            try
            {
                bool success = false;

                var objCat = new Category();
                objCat.Name = catRequest.Name;
                objCat.Sequence = catRequest.Sequence;
                objCat.Icon = catRequest.Icon;
                objCat.Active = catRequest.Active;

                using (var repo = new RepositoryPattern<Category>())
                {
                    repo.Insert(objCat);
                    repo.Save();
                    success = true;
                }
                return success;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Deletes a category
        /// </summary>
        /// <param name="categoryId">The unique id of the category that needs to be deleted</param>
        /// <returns>Boolean indication if the deletion was successfull</returns>
        public bool DeleteCategory(int categoryId)
        {
            try
            {
                bool success = false;
                using (var repo = new RepositoryPattern<Category>())
                {
                    var objCat = _mapper.Map<Category>(repo.SelectByID(categoryId));
                    if (objCat != null)
                    {
                        repo.Delete(objCat);
                        repo.Save();
                        success = true;
                    }
                    return success;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Retrieves a specific category 
        /// </summary>
        /// <param name="categoryId">The unique id of the category</param>
        /// <returns>The category object</returns>
        public Task<CategoryModel> GetCategoryById(int categoryId)
        {
            try
            {
                var dataToReturn = new CategoryModel();

                var p_repo = new RepositoryPattern<Permission>();
                var m_repo = new RepositoryPattern<Module>();

                using (var repo = new RepositoryPattern<Category>())
                {
                    var category = _mapper.Map<CategoryModel>(repo.SelectByID(categoryId));
                    dataToReturn = category;
                }
                return Task.FromResult(dataToReturn);
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Updates a category
        /// </summary>
        /// <param name="catRequest">The category update request</param>
        /// <returns>Boolean indication if the update was successfull or not</returns>
        public bool UpdateCategory(CategoryUpdateRequest catRequest)
        {
            try
            {
                bool success = false;

                var objCat = _context.Categories.Where(a => a.CategoryId == catRequest.CategoryId).SingleOrDefault();
                if (objCat != null)
                {
                   if(catRequest.Name != null) objCat.Name = catRequest.Name;
                   if(catRequest.Icon != null) objCat.Icon = catRequest.Icon;
                   if(catRequest.Sequence != 0) objCat.Sequence = catRequest.Sequence;
                   if(catRequest.Active != null) objCat.Active = catRequest.Active;

                    using (var repo = new RepositoryPattern<Category>())
                    {
                        repo.Update(objCat);
                        repo.Save();
                        success = true;
                    }
                }
                return success;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

