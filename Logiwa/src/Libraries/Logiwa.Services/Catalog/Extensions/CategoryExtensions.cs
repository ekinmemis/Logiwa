using Logiwa.Core.Domain.Catalog;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Logiwa.Services.Catalog.Extensions
{
    public static class CategoryExtensions
    {
        public static IList<Category> SortCategoriesForTree(this IList<Category> source, int parentId = 0)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            var result = new List<Category>();

            foreach (var cat in source.Where(c => c.ParentCategoryId == parentId).ToList())
            {
                result.Add(cat);
                result.AddRange(SortCategoriesForTree(source, cat.Id));
            }
            if (result.Count != source.Count)
            {
                foreach (var cat in source)
                    if (result.FirstOrDefault(x => x.Id == cat.Id) == null)
                        result.Add(cat);
            }
            return result;
        }

        public static ProductCategory FindProductCategory(this IList<ProductCategory> source,
            int productId, int categoryId)
        {
            foreach (var productCategory in source)
                if (productCategory.ProductId == productId && productCategory.CategoryId == categoryId)
                    return productCategory;

            return null;
        }
    }
}
