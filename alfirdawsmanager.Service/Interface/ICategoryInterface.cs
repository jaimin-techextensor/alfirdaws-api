using System;
using alfirdawsmanager.Service.Models;
using alfirdawsmanager.Service.Models.RequestModels;

namespace alfirdawsmanager.Service.Interface
{
    public interface ICategoryInterface
    {
        Task<List<CategoryModel>> GetCategoriesOverview();
        Task<CategoryModel> GetCategoryById(int categoryId);
        bool CreateCategory(CategoryCreateRequest catRequest);
        bool UpdateCategory(CategoryUpdateRequest catRequest);
        bool DeleteCategory(int categoryId);

        Task<List<SubCategoryModel>> GetSubCategories(int categoryId);
        Task<SubCategoryModel> GetSubCategoryById(int subcategoryId);
        bool CreateSubCategory(int categoryId, SubCategoryCreateRequest subCatRequest);
        bool UpdateSubCategory(int categoryId, SubCategoryUpdateRequest subCatRequest);
        bool DeleteSubCategory(int subcategoryId);
    }
}

