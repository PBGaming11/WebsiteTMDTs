using Microsoft.AspNetCore.Mvc;
using WebsiteTMDT.Areas.Admin.Models.EF;
using WebsiteTMDT.Data;

namespace WebsiteTMDT.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductCategorysController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ProductCategorysController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var items = _db.ProductCategories;
            return View(items);
        }
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(ProductCategory model)
        {
            if (ModelState.IsValid)
            {
                return View(model);
            }

            model.CreateDate = DateTime.Now;
            model.ModifierDate = DateTime.Now;
            model.Alias = WebsiteTMDT.Areas.Admin.Models.Common.Filter.FilterChar(model.Title);
            _db.ProductCategories.Add(model);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            var item = _db.ProductCategories.Find(id);
            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ProductCategory model)
        {
            if (ModelState.IsValid)
            {
                return View(model);
            }
            _db.ProductCategories.Attach(model);
            model.ModifierDate = DateTime.Now;
            model.Alias = WebsiteTMDT.Areas.Admin.Models.Common.Filter.FilterChar(model.Title);
            _db.Entry(model).Property(x => x.Title).IsModified = true;
            _db.Entry(model).Property(x => x.Description).IsModified = true;
            _db.Entry(model).Property(x => x.Alias).IsModified = true;
            _db.Entry(model).Property(x => x.SeoDescription).IsModified = true;
            _db.Entry(model).Property(x => x.SeoKeywords).IsModified = true;
            _db.Entry(model).Property(x => x.SeoTitle).IsModified = true;
            _db.Entry(model).Property(x => x.ModifierBy).IsModified = true;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var item = _db.ProductCategories.Find(id);
            if (item != null)
            {
                //var DeleteItem = _db.Categories.Attach(item);
                _db.ProductCategories.Remove(item);
                _db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });

        }
    }
}
