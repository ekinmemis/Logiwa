using Logiwa.Core.Domain.Catalog;

namespace Logiwa.Data.Mapping.Catalog
{
    public partial class CategoryMap : LogiwaEntityTypeConfiguration<Category>
    {
        public CategoryMap()
        {
            this.ToTable(nameof(Category));
            this.HasKey(c => c.Id);
            this.Property(c => c.Name).IsRequired().HasMaxLength(400);
        }
    }
}
