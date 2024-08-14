using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebsiteTMDT.Areas.Admin.Models.EF;
using WebsiteTMDT.Data;
using X.PagedList.Extensions;

namespace WebsiteTMDT.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NewsController : Controller
    {
        private readonly ApplicationDbContext _db;
        public NewsController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index(string searchText, int? page)
        {
            var pageSize = 10;
            var pageIndex = page ?? 1;

            // Start with the base query
            IEnumerable<New> items = _db.News.OrderByDescending(x => x.Id);

            // Apply search filter
            if (!string.IsNullOrEmpty(searchText))
            {
                items = items.Where(x => x.Alias.Contains(searchText) || x.Title.Contains(searchText));
            }

            // Apply pagination after filtering
            var pagedItems = items.ToPagedList(pageIndex, pageSize);

            ViewBag.PageSize = pageSize;
            ViewBag.Page = pageIndex;

            return View(pagedItems);
        }
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(New model)
        {
            if (ModelState.IsValid)
            {
                return View(model);
            }

            model.CreateDate = DateTime.Now;
            model.ModifierDate = DateTime.Now;
            model.CategoryId = 1;
            model.Alias = WebsiteTMDT.Areas.Admin.Models.Common.Filter.FilterChar(model.Title);
            _db.News.Add(model);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            var item = _db.News.Find(id);
            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(New model)
        {
            if (ModelState.IsValid)
            {
                return View(model);
            }

            model.ModifierDate = DateTime.Now;
            model.Alias = WebsiteTMDT.Areas.Admin.Models.Common.Filter.FilterChar(model.Title);
            _db.News.Attach(model);
            _db.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var item = _db.News.Find(id);
            if(item != null)
            {
                _db.News.Remove(item);
                _db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
        //chỉnh hiện thị trong tin tức
        [HttpPost]
        public IActionResult IsActive(int id)
        {
            var item = _db.News.Find(id);
            if (item != null)
            {
                item.IsActive = !item.IsActive;
                _db.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _db.SaveChanges();
                return Json(new { success = true, isActive = item.IsActive });
            }
            return Json(new { success = false });
        }
        [HttpPost]
        public ActionResult DeleteAll(string id)
        {
            if(!string.IsNullOrEmpty(id))
            {
                var items = id.Split(",");
                if(items!= null && items.Any())
                {
                    foreach(var item in items)
                    {
                        var obj = _db.News.Find(Convert.ToInt32(item));
                        _db.News.Remove(obj);
                        _db.SaveChanges();
                    }
                }
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
    }
}
