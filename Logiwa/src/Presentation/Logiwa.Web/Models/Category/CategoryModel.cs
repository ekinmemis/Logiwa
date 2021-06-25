using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Logiwa.Web.Models.Category
{
    public class CategoryModel
    {
        public CategoryModel()
        {
            AvaliableParentCategories = new List<SelectListItem>();
        }
        
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool Deleted { get; set; }

        public DateTime CreatedOnUtc { get; set; }

        public DateTime UpdatedOnUtc { get; set; }

        public int ParentCategoryId { get; set; }
        public IList<SelectListItem> AvaliableParentCategories { get; set; }
    }
}
