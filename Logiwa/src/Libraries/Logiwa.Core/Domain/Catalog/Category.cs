using System;

namespace Logiwa.Core.Domain.Catalog
{
    public partial class Category : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int ParentCategoryId { get; set; }

        public bool Deleted { get; set; }

        public DateTime CreatedOnUtc { get; set; }

        public DateTime UpdatedOnUtc { get; set; }
    }
}
