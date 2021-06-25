
using Logiwa.Core.Domain.Catalog;
using Logiwa.Web.Models.Product;

namespace Logiwa.Web.Factories
{
    public partial interface IProductModelFactory
    {
        void PrepareProductModel(ProductModel model, Product product);

        ProductListModel PrepareProductListModel(ProductPagingFilteringModel command);
    }
}