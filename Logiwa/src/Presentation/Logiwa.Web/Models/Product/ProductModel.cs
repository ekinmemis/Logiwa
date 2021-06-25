
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Logiwa.Web.Models.Product
{
    public class ProductModel
    {
        public ProductModel()
        {
            AvailableProductTypes = new List<SelectListItem>();

            SelectedCategoryIds = new List<int>();
            AvailableCategories = new List<SelectListItem>();
        }

        public int Id { get; set; }

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

        public IList<SelectListItem> AvailableProductTypes { get; set; }

        public List<int> SelectedCategoryIds { get; set; }
        public IList<SelectListItem> AvailableCategories { get; set; }
    }
}
