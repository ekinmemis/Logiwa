using Logiwa.Core;
using Logiwa.Core.Domain.Catalog;
using Logiwa.Services.Catalog;
using Logiwa.Web.Models.Product;

using System.Linq;

namespace Logiwa.Web.Factories
{
    public partial class ProductModelFactory : IProductModelFactory
    {
        private readonly IProductService _productService;

        public ProductModelFactory(IProductService productService)
        {
            _productService = productService;
        }

        public virtual void PrepareProductModel(ProductModel model, Product product)
        {
            model.Id = product.Id;
            model.ProductTypeId = product.ProductTypeId;
            model.Name = product.Name;
            model.Sku = product.Sku;
            model.ShortDescription = product.ShortDescription;
            model.Price = product.Price;
            model.CreatedOnUtc = product.CreatedOnUtc;
            model.UpdatedOnUtc = product.UpdatedOnUtc;
            model.Weight = product.Weight;
            model.Length = product.Length;
            model.Width = product.Width;
            model.Height = product.Height;
            model.Deleted = product.Deleted;
        }

        public virtual ProductListModel PrepareProductListModel(ProductPagingFilteringModel command)
        {
            var model = new ProductListModel();

            if (command.PageSize <= 0) command.PageSize = 20;
            if (command.PageNumber <= 0) command.PageNumber = 1;

            IPagedList<Product> product = _productService.GetAllProducts(command.SearchName, command.CategoryId, command.PageNumber - 1, command.PageSize);

            model.PagingFilteringContext.LoadPagedList(product);

            model.Products = product.Select(x =>
            {
                var productPostModel = new ProductModel();
                PrepareProductModel(productPostModel, x);
                return productPostModel;
            }).ToList();


            return model;
        }
    }
}
