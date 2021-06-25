using System.Collections.Generic;

namespace Logiwa.Web.Models.Product
{
    public class ProductListModel : DataTableRequestModel
    {
        public ProductListModel()
        {
            PagingFilteringContext = new ProductPagingFilteringModel();
            Products = new List<ProductModel>();
        }

        public string SearchName { get; set; }

        public int CategoryId { get; set; }

        public ProductPagingFilteringModel PagingFilteringContext { get; set; }

        public IList<ProductModel> Products { get; set; }
    }
}
