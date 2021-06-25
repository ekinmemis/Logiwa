using Logiwa.Core;
using Logiwa.Core.Domain.Catalog;

using System.Collections.Generic;

namespace Logiwa.Services.Catalog
{
    public partial interface IProductService
    {
        void DeleteProduct(Product product);

        void DeleteProducts(IList<Product> products);

        Product GetProductById(int productId);

        IList<Product> GetProductsByIds(int[] productIds);

        void InsertProduct(Product product);

        void UpdateProduct(Product product);

        void UpdateProducts(IList<Product> products);

        Product GetProductBySku(string sku);

        IPagedList<Product> GetAllProducts(
            string name = "",
            int categoryId = 0,
             int pageIndex = 0,
             int pageSize = int.MaxValue,
             IList<int> categoryIds = null,
             ProductType? productType = null,
             decimal? priceMin = null,
             decimal? priceMax = null);

        int GetNumberOfProductsInCategory(IList<int> categoryIds = null);

        IList<Product> GetProductsBySku(string[] skuArray);

        void InsertProductCategory(int productId, List<int> categoryIds);

        Product GetLast();
    }
}
