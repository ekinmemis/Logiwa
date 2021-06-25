using Logiwa.Core.Domain.Catalog;

namespace Logiwa.Data.Mapping.Catalog
{
    public partial class ProductMap : LogiwaEntityTypeConfiguration<Product>
    {
        public ProductMap()
        {
            this.ToTable(nameof(Product));
            this.HasKey(p => p.Id);
            this.Property(p => p.Name).IsRequired().HasMaxLength(400);
            this.Property(p => p.Price).HasPrecision(18, 4);
            this.Property(p => p.Weight).HasPrecision(18, 4);
            this.Property(p => p.Length).HasPrecision(18, 4);
            this.Property(p => p.Width).HasPrecision(18, 4);
            this.Property(p => p.Height).HasPrecision(18, 4);

            this.Ignore(p => p.ProductType);
        }
    }
}
