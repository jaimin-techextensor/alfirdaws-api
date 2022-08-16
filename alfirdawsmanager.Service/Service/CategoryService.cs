using System;
using alfirdawsmanager.Data.Models;
using alfirdawsmanager.Data.Repository;
using alfirdawsmanager.Service.Interface;
using alfirdawsmanager.Service.Models;
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
                    List<Category> categories = repo.SelectAll().OrderBy(c => c.CategoryId).ToList();

                    foreach (var category in categories)
                    {
                        CategoryModel catModel = new CategoryModel();
                        catModel.CategoryId = category.CategoryId;
                        catModel.Name = category.Name;
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


        public bool CreateCategory(CategoryModel catModel)
        {
            throw new NotImplementedException();
        }

        public bool DeleteCategory(int categoryId)
        {
            throw new NotImplementedException();
        }

        public Task<CategoryModel> GetCategoryById(int categoryId)
        {
            throw new NotImplementedException();
        }

        public bool UpdateCategory(CategoryModel catModel)
        {
            throw new NotImplementedException();
        }
    }
}

