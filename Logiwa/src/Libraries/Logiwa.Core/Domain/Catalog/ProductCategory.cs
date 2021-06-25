namespace Logiwa.Core.Domain.Catalog
{
    public partial class ProductCategory : BaseEntity
    {
        public int ProductId { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public virtual Product Product { get; set; }
    }
}
