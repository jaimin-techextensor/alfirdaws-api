using System;
using alfirdawsmanager.Service.Models;

namespace alfirdawsmanager.Service.Interface
{
    public interface ICategoryInterface
    {
        Task<List<CategoryModel>> GetCategoriesOverview();
        Task<CategoryModel> GetCategoryById(int categoryId);
        bool CreateCategory(CategoryModel catModel);
        bool UpdateCategory(CategoryModel catModel);
        bool DeleteCategory(int categoryId);
    }
}

