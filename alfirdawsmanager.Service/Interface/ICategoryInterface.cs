using System;
using alfirdawsmanager.Service.Models;
using alfirdawsmanager.Service.Models.RequestModels;

namespace alfirdawsmanager.Service.Interface
{
    public interface ICategoryInterface
    {
        Task<List<CategoryModel>> GetCategoriesOverview();
        Task<CategoryModel> GetCategoryById(int categoryId);
        bool CreateCategory(CategoryCreateRequest catModel);
        bool UpdateCategory(CategoryUpdateRequest catModel);
        bool DeleteCategory(int categoryId);
    }
}

