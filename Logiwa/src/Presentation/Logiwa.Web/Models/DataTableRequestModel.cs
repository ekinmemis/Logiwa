namespace Logiwa.Web.Models
{
    public class DataTableRequestModel
    {
        public string Draw { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }
    }
}
