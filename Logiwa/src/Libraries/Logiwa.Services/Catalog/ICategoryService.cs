using Logiwa.Core;
using Logiwa.Core.Domain.Catalog;

using System.Collections.Generic;

namespace Logiwa.Services.Catalog
{
    public partial interface ICategoryService
    {
        void DeleteCategory(Category category);

        IPagedList<Category> GetAllCategories(string categoryName = "",
            int pageIndex = 0, int pageSize = int.MaxValue);

        IList<Category> GetAllCategoriesByParentCategoryId(int parentCategoryId,
            bool showHidden = false, bool includeAllLevels = false);

        Category GetCategoryById(int categoryId);

        void InsertCategory(Category category);

        void UpdateCategory(Category category);

        void DeleteProductCategory(ProductCategory productCategory);

        IPagedList<ProductCategory> GetProductCategoriesByCategoryId(int categoryId,
            int pageIndex = 0, int pageSize = int.MaxValue);

        IList<ProductCategory> GetProductCategoriesByProductId(int productId);

        ProductCategory GetProductCategoryById(int productCategoryId);

        void InsertProductCategory(ProductCategory productCategory);

        void UpdateProductCategory(ProductCategory productCategory);

        string[] GetNotExistingCategories(string[] categoryNames);

        IDictionary<int, int[]> GetProductCategoryIds(int[] productIds);
    }
}
