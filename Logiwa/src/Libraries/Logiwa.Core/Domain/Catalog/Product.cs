using System;
using System.Collections.Generic;

namespace Logiwa.Core.Domain.Catalog
{
    public partial class Product : BaseEntity
    {
        private ICollection<ProductCategory> _productCategories;

        public int ProductTypeId { get; set; }

        public string Name { get; set; }

        public string Sku { get; set; }

        public string ShortDescription { get; set; }

        public decimal Price { get; set; }

        public DateTime CreatedOnUtc { get; set; }

        public DateTime UpdatedOnUtc { get; set; }

        public decimal Weight { get; set; }

        public decimal Length { get; set; }

        public decimal Width { get; set; }

        public decimal Height { get; set; }

        public bool Deleted { get; set; }

        public ProductType ProductType
        {
            get
            {
                return (ProductType)this.ProductTypeId;
            }
            set
            {
                this.ProductTypeId = (int)value;
            }
        }

        public virtual ICollection<ProductCategory> ProductCategories
        {
            get { return _productCategories ?? (_productCategories = new List<ProductCategory>()); }
            protected set { _productCategories = value; }
        }
    }
}
