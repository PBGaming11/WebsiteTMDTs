using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebsiteTMDT.Areas.Admin.Models.EF;
using WebsiteTMDT.Data;

namespace WebsiteTMDT.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var items = _db.Categories;
            return View(items);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(Category model)
        {
            if (ModelState.IsValid)
            {
                return View(model);
            }

            model.CreateDate = DateTime.Now;
            model.ModifierDate = DateTime.Now;
            model.Alias = WebsiteTMDT.Areas.Admin.Models.Common.Filter.FilterChar(model.Title);
            _db.Categories.Add(model);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            var item = _db.Categories.Find(id);
            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category model)
        {
            if (ModelState.IsValid)
            {
                return View(model);
            }

            _db.Categories.Attach(model);
            model.ModifierDate = DateTime.Now;
            model.Alias = WebsiteTMDT.Areas.Admin.Models.Common.Filter.FilterChar(model.Title);
            _db.Entry(model).Property(x => x.Title).IsModified = true;
            _db.Entry(model).Property(x => x.Description).IsModified = true;
            _db.Entry(model).Property(x => x.Alias).IsModified = true;
            _db.Entry(model).Property(x => x.SeoDescription).IsModified = true;
            _db.Entry(model).Property(x => x.SeoKeywords).IsModified = true;
            _db.Entry(model).Property(x => x.SeoTitle).IsModified = true;
            _db.Entry(model).Property(x => x.Position).IsModified = true;
            _db.Entry(model).Property(x => x.ModifierBy).IsModified = true;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var item = _db.Categories.Find(id);
            if (item != null)
            {
                //var DeleteItem = _db.Categories.Attach(item);
                _db.Categories.Remove(item);
                _db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });

        }


    
    }
}
