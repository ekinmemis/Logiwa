using Logiwa.Core;
using Logiwa.Core.Domain.Catalog;
using Logiwa.Data;

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Logiwa.Services.Catalog
{
    public partial class ProductService : IProductService
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<ProductCategory> _productCategoryRepository;

        private readonly IDbContext _dbContext;

        public ProductService(IRepository<Product> productRepository,
            IDbContext dbContext,
            IRepository<ProductCategory> productCategoryRepository)
        {
            this._productRepository = productRepository;
            this._dbContext = dbContext;
            _productCategoryRepository = productCategoryRepository;
        }

        public virtual void DeleteProduct(Product product)
        {
            if (product == null)
                throw new ArgumentNullException("product");

            product.Deleted = true;
            UpdateProduct(product);
        }

        public virtual void DeleteProducts(IList<Product> products)
        {
            if (products == null)
                throw new ArgumentNullException("products");

            foreach (var product in products)
            {
                product.Deleted = true;
            }
            UpdateProducts(products);
        }

        public virtual Product GetProductById(int productId)
        {
            if (productId == 0)
                return null;

            return _productRepository.GetById(productId);
        }

        public virtual IList<Product> GetProductsByIds(int[] productIds)
        {
            if (productIds == null || productIds.Length == 0)
                return new List<Product>();

            var query = from p in _productRepository.Table
                        where productIds.Contains(p.Id) && !p.Deleted
                        select p;
            var products = query.ToList();
            var sortedProducts = new List<Product>();
            foreach (int id in productIds)
            {
                var product = products.Find(x => x.Id == id);
                if (product != null)
                    sortedProducts.Add(product);
            }
            return sortedProducts;
        }

        public virtual void InsertProduct(Product product)
        {
            if (product == null)
                throw new ArgumentNullException("product");

            _productRepository.Insert(product);
        }

        public virtual void UpdateProduct(Product product)
        {
            if (product == null)
                throw new ArgumentNullException("product");

            _productRepository.Update(product);
        }

        public virtual void UpdateProducts(IList<Product> products)
        {
            if (products == null)
                throw new ArgumentNullException("products");

            _productRepository.Update(products);
        }

        public virtual int GetNumberOfProductsInCategory(IList<int> categoryIds = null)
        {
            if (categoryIds != null && categoryIds.Contains(0))
                categoryIds.Remove(0);

            var query = _productRepository.Table;
            query = query.Where(p => !p.Deleted);
            if (categoryIds != null && categoryIds.Any())
            {
                query = from p in query
                        from pc in p.ProductCategories.Where(pc => categoryIds.Contains(pc.CategoryId))
                        select p;
            }

            var result = query.Select(p => p.Id).Distinct().Count();
            return result;
        }

        public virtual Product GetProductBySku(string sku)
        {
            if (String.IsNullOrEmpty(sku))
                return null;

            sku = sku.Trim();

            var query = from p in _productRepository.Table
                        orderby p.Id
                        where !p.Deleted &&
                        p.Sku == sku
                        select p;
            var product = query.FirstOrDefault();
            return product;
        }

        public IList<Product> GetProductsBySku(string[] skuArray)
        {
            if (skuArray == null)
                throw new ArgumentNullException("skuArray");

            var query = _productRepository.Table;

            query = query.Where(p => !p.Deleted && skuArray.Contains(p.Sku));

            return query.ToList();
        }

        public virtual IPagedList<Product> GetAllProducts(
            string name = "",
            int categoryId = 0,
             int pageIndex = 0,
             int pageSize = int.MaxValue,
             IList<int> categoryIds = null,
             ProductType? productType = null,
             decimal? priceMin = null,
             decimal? priceMax = null)
        {
            if (categoryIds != null && categoryIds.Contains(0))
                categoryIds.Remove(0);

            var query = _productRepository.Table;

            query = query.Where(p => !p.Deleted);

            var nowUtc = DateTime.UtcNow;

            if (productType.HasValue)
            {
                var productTypeId = (int)productType.Value;
                query = query.Where(p => p.ProductTypeId == productTypeId);
            }

            if (priceMin.HasValue)
            {
                query = query.Where(p => p.Price >= priceMin.Value);
            }

            if (priceMax.HasValue)
            {
                query = query.Where(p => p.Price <= priceMax.Value);
            }

            if (categoryIds != null && categoryIds.Any())
            {
                query = from p in query
                        from pc in p.ProductCategories.Where(pc => categoryIds.Contains(pc.CategoryId))
                        select p;
            }

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(f => f.Name.Contains(name));
            }

            if (categoryId > 0)
            {
                query = from p in query
                        from pc in p.ProductCategories.Where(pc => pc.CategoryId == categoryId)
                        select p;
            }

            //query = from p in query
            //        group p by p.Id
            //        into pGroup
            //        orderby pGroup.Key
            //        select pGroup.FirstOrDefault();

            query = query.OrderBy(p => p.Name);

            var products = new PagedList<Product>(query, pageIndex, pageSize);
            return products;
        }

        public List<Product> GetAll()
        {
            return _productRepository.Table.ToList();
        }

        public void InsertProductCategory(int productId, List<int> categoryIds)
        {
            categoryIds.ForEach(categoryId =>
            {
                _productCategoryRepository.Insert(new ProductCategory { CategoryId = categoryId, ProductId = productId });
            });
        }

        public Product GetLast() => _productRepository.Table.OrderByDescending(f => f.Id).FirstOrDefault();
    }
}
