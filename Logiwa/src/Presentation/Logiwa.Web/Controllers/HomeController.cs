namespace Logiwa.Web.Controllers
{
    using Logiwa.Core.Domain.Catalog;
    using Logiwa.Services.Catalog;
    using Logiwa.Web.Configurations;
    using Logiwa.Web.Factories;
    using Logiwa.Web.Models.Product;

    using System.Web.Mvc;

    public class HomeController : Controller
    {
        private readonly IProductModelFactory _productModelFactory;
        private readonly IProductService _productService;

        public HomeController(IProductModelFactory productModelFactory, IProductService productService)
        {
            _productModelFactory = productModelFactory;
            _productService = productService;
        }

        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public ActionResult List(ProductPagingFilteringModel command)
        {
            var model = _productModelFactory.PrepareProductListModel(command);
            return View(model);
        }

        public ActionResult Single(int id)
        {
            var product = _productService.GetProductById(id);
            var model = product.MapTo<Product, ProductModel>();
            return View(model);
        }
    }
}
