using Logiwa.Core.Domain.Catalog;
using Logiwa.Services.Catalog;
using Logiwa.Web.Configurations;
using Logiwa.Web.Models.Product;

using System;
using System.Web.Mvc;

namespace Logiwa.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public ActionResult List()
        {
            return View(new ProductListModel());
        }

        [HttpPost]
        public ActionResult List(ProductListModel model)
        {
            var products = _productService.GetAllProducts(
                name: model.SearchName,
                pageIndex: model.PageIndex,
                pageSize: model.PageSize);

            return Json(new
            {
                draw = model.Draw,
                recordsFiltered = 0,
                recordsTotal = products.TotalCount,
                data = products
            });
        }

        public ActionResult Create()
        {
            var model = new ProductModel();

            model.AvailableProductTypes.Add(new SelectListItem() { Text = "Simple Product", Value = Convert.ToInt32(ProductType.SimpleProduct).ToString() });
            model.AvailableProductTypes.Add(new SelectListItem() { Text = "Grouped Product", Value = Convert.ToInt32(ProductType.GroupedProduct).ToString() });

            foreach (var item in _categoryService.GetAllCategories())
                model.AvailableCategories.Add(new SelectListItem() { Text = item.Name, Value = item.Id.ToString() });

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(ProductModel model)
        {
            if (ModelState.IsValid)
            {
                Product product = model.MapTo<ProductModel, Product>();
                product.CreatedOnUtc = DateTime.Now;
                product.UpdatedOnUtc = DateTime.Now;

                _productService.InsertProduct(product);
                var lastAdded = _productService.GetLast();
                _productService.InsertProductCategory(lastAdded.Id, model.SelectedCategoryIds);
            }

            return RedirectToAction("List");
        }

        public ActionResult Edit(int id)
        {
            var product = _productService.GetProductById(id);

            if (product == null || product.Deleted)
                return RedirectToAction("List");

            var model = product.MapTo<Product, ProductModel>();

            model.AvailableProductTypes.Add(new SelectListItem() { Text = "Simple Product", Value = ProductType.SimpleProduct.ToString() });
            model.AvailableProductTypes.Add(new SelectListItem() { Text = "Grouped Product", Value = ProductType.GroupedProduct.ToString() });

            foreach (var item in _categoryService.GetAllCategories())
                model.AvailableCategories.Add(new SelectListItem() { Text = item.Name, Value = item.Id.ToString() });

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(ProductModel model)
        {
            if (ModelState.IsValid)
            {
                var product = _productService.GetProductById(model.Id);
                if (product == null || product.Deleted)
                    return RedirectToAction("List");

                //automapper key error here
                model.ProductTypeId = product.ProductTypeId;
                model.Name = product.Name;
                model.Sku = product.Sku;
                model.ShortDescription = product.ShortDescription;
                model.Price = product.Price;
                model.CreatedOnUtc = product.CreatedOnUtc;
                model.UpdatedOnUtc = product.UpdatedOnUtc;
                model.Weight = product.Weight;
                model.Length = product.Length;
                model.Width = product.Width;
                model.Height = product.Height;
                model.Deleted = product.Deleted;

                _productService.UpdateProduct(product);
            }

            return RedirectToAction("List");
        }

        public ActionResult Delete(int id)
        {
            var product = _productService.GetProductById(id);

            if (product == null)
                return Json("ERR");

            _productService.DeleteProduct(product);

            return RedirectToAction("List");
        }
    }
}