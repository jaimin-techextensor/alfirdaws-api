using System;
using alfirdawsmanager.Data.Models;
using alfirdawsmanager.Data.Repository;
using alfirdawsmanager.Service.Interface;
using alfirdawsmanager.Service.Models;
using alfirdawsmanager.Service.Models.RequestModels;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

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
               // var dataToReturn = new List<CategoryModel>();
               // var sub_repo = new RepositoryPattern<SubCategory>();

               // using (var repo = new RepositoryPattern<Category>())
               // {
                    //List<Category> categories = repo.SelectAll().OrderBy(c => c.Sequence).ToList();

                    List<CategoryModel> categories = _context.Categories
                                                    .Include(s => s.SubCategories)
                                                    .Select(c => new CategoryModel
                                                    {
                                                        CategoryId = c.CategoryId,
                                                        Sequence = c.Sequence,
                                                        Active = c.Active,
                                                        Icon = c.Icon,
                                                        Name = c.Name,
                                                        CountSubcategories = c.SubCategories.Count(),
                                                        Subcategories = _mapper.Map<List<SubCategoryModel>>(c.SubCategories.ToList())
                                                       
                                                    }
                                                    ).ToList();

                    //foreach (var category in categories)
                    //{
                    //    CategoryModel catModel = new CategoryModel();
                    //    catModel.CategoryId = category.CategoryId;
                    //    catModel.Name = category.Name;
                    //    catModel.Sequence = category.Sequence;
                    //    catModel.Icon = category.Icon;
                    //    catModel.Active = category.Active;
                    //    catModel.CountSubcategories = sub_repo.SelectAll().Where(c => c.CategoryId == category.CategoryId).Count(); 

                    //    dataToReturn.Add(catModel);
                    //}
                    //return Task.FromResult(dataToReturn);
                    return Task.FromResult(categories); 
               // }
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
                        repo.Delete(categoryId);
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
                CategoryModel categories;
                categories = _context.Categories
                                                    .Include(s => s.SubCategories)
                                                    .Where(s => s.CategoryId == categoryId)
                                                    .Select(c => new CategoryModel
                                                    {
                                                        CategoryId = c.CategoryId,
                                                        Sequence = c.Sequence,
                                                        Active = c.Active,
                                                        Icon = c.Icon,
                                                        Name = c.Name,
                                                        CountSubcategories = c.SubCategories.Count()
                                                    }
                                                    ).SingleOrDefault();

                return Task.FromResult(categories);
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

        /// <summary>
        /// Retrieves all the subcategories for a specific category
        /// </summary>
        /// <param name="categoryId">The unique category id</param>
        /// <returns>List of subcategories</returns>
        public Task<List<SubCategoryModel>> GetSubCategories(int categoryId)
        {
            try
            {
                var dataToReturn = new List<SubCategoryModel>();

                using (var repo = new RepositoryPattern<SubCategory>())
                {
                    dataToReturn = _mapper.Map <List<SubCategoryModel>> (repo.SelectAll()
                                                                             .Where(s => s.CategoryId == categoryId)
                                                                             .OrderBy(c => c.Sequence)
                                                                             .ToList()
                                                                        );
                    return Task.FromResult(dataToReturn);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Create a new subcategory 
        /// </summary>
        /// <param name="subCatRequest">The subcategory request</param>
        /// <returns>Boolean indication if the creation was successfull</returns>
        public bool CreateSubCategory(int categoryId, SubCategoryCreateRequest subCatRequest)
        {
            try
            {
                bool success = false;

                var objSubCat = new SubCategory();
                objSubCat.CategoryId = categoryId;
                objSubCat.Name = subCatRequest.Name;
                objSubCat.Sequence = subCatRequest.Sequence;
                objSubCat.Icon = subCatRequest.Icon;
                objSubCat.Active = subCatRequest.Active;

                using (var repo = new RepositoryPattern<SubCategory>())
                {
                    repo.Insert(objSubCat);
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
        /// Updates a subcategory for a specific category
        /// </summary>
        /// <param name="categoryId">The id of the category </param>
        /// <param name="subCatRequest">The category update request</param>
        /// <returns>Boolean indication if the update was successfull or not</returns>
        public bool UpdateSubCategory(int categoryId, SubCategoryUpdateRequest subCatRequest)
        {
            try
            {
                bool success = false;

                var objSubCat = _context.SubCategories.Where(a => a.SubCategoryId == subCatRequest.SubCategoryId).SingleOrDefault();
                if (objSubCat != null)
                {
                    objSubCat.CategoryId = categoryId;
                    if (subCatRequest.Name != null) objSubCat.Name = subCatRequest.Name;
                    if (subCatRequest.Icon != null) objSubCat.Icon = subCatRequest.Icon;
                    if (subCatRequest.Sequence != 0) objSubCat.Sequence = subCatRequest.Sequence;
                    if (subCatRequest.Active != null) objSubCat.Active = subCatRequest.Active;

                    using (var repo = new RepositoryPattern<SubCategory>())
                    {
                        repo.Update(objSubCat);
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


        /// <summary>
        /// Retrieves a specific subcategory 
        /// </summary>
        /// <param name="subcategoryId">The unique id of the subcategory</param>
        /// <returns>The subcategory object</returns>
        public Task<SubCategoryModel> GetSubCategoryById( int subcategoryId)
        {
            try
            {
                var dataToReturn = new SubCategoryModel();

                using (var repo = new RepositoryPattern<SubCategory>())
                {
                    var subcategory = _mapper.Map<SubCategoryModel>(repo.SelectByID(subcategoryId));

                    if(subcategory != null)
                    {
                        dataToReturn = subcategory;
                    }
                }
                return Task.FromResult(dataToReturn);
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Delete a subcategory 
        /// </summary>
        /// <param name="subcategoryId">Unique id of the subcategory</param>
        /// <returns>Boolean indication if the deletion was succesfull or not</returns>
        public bool DeleteSubCategory(int subcategoryId)
        {
            try
            {
                bool success = false;
                using (var repo = new RepositoryPattern<SubCategory>())
                {
                    var objSubCat = _mapper.Map<SubCategory>(repo.SelectByID(subcategoryId));
                    if (objSubCat != null)
                    {
                        repo.Delete(objSubCat.SubCategoryId);
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
    }
}

