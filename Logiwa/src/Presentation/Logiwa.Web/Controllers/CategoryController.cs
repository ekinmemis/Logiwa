using Logiwa.Core.Domain.Catalog;
using Logiwa.Services.Catalog;
using Logiwa.Web.Configurations;
using Logiwa.Web.Models.Category;

using System;
using System.Web.Mvc;

namespace Logiwa.Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public ActionResult List()
        {
            return View(new CategoryListModel());
        }

        [HttpPost]
        public ActionResult List(CategoryListModel model)
        {
            var categorys = _categoryService.GetAllCategories(
                categoryName: model.SearchName,
                pageIndex: model.PageIndex,
                pageSize: model.PageSize);

            return Json(new
            {
                draw = model.Draw,
                recordsFiltered = 0,
                recordsTotal = categorys.TotalCount,
                data = categorys
            });
        }

        public ActionResult Create()
        {
            var model = new CategoryModel() { };

            foreach (var item in _categoryService.GetAllCategories())
                model.AvaliableParentCategories.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.Name });

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(CategoryModel model)
        {
            if (ModelState.IsValid)
            {
                model.CreatedOnUtc = DateTime.Now;
                model.UpdatedOnUtc = DateTime.Now;

                Category category = model.MapTo<CategoryModel, Category>();

                _categoryService.InsertCategory(category);
            }

            return RedirectToAction("List");
        }

        public ActionResult Edit(int id)
        {
            var category = _categoryService.GetCategoryById(id);

            if (category == null || category.Deleted)
                return RedirectToAction("List");

            var model = category.MapTo<Category, CategoryModel>();

            foreach (var item in _categoryService.GetAllCategories())
                model.AvaliableParentCategories.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.Name, Selected = item.Id == category.ParentCategoryId });

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(CategoryModel model)
        {
            if (ModelState.IsValid)
            {
                var category = _categoryService.GetCategoryById(model.Id);

                if (category == null || category.Deleted)
                    return RedirectToAction("List");

                category.Id = model.Id;
                category.UpdatedOnUtc = DateTime.Now;
                category.Name = model.Name;
                category.Description = model.Description;
                category.ParentCategoryId = model.ParentCategoryId;

                _categoryService.UpdateCategory(category);
            }

            return RedirectToAction("List");
        }

        public ActionResult Delete(int id)
        {
            var category = _categoryService.GetCategoryById(id);

            if (category == null)
                return Json("ERR");

            _categoryService.DeleteCategory(category);

            return RedirectToAction("List");
        }
    }
}
