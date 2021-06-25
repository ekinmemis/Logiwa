using Logiwa.Web.Framework.UI.Paging;

namespace Logiwa.Web.Models.Product
{
    public partial class ProductPagingFilteringModel : BasePageableModel
    {
        public string SearchName { get; set; }

        public int CategoryId { get; set; }
    }
}