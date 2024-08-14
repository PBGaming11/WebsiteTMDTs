using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebsiteTMDT.Areas.Admin.Models.EF;
using WebsiteTMDT.Data;
using X.PagedList.Extensions;

namespace WebsiteTMDT.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PostsController : Controller
    {
        private readonly ApplicationDbContext _db;
        public PostsController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index(string searchText, int? page)
        {
            var pageSize = 10;
            var pageIndex = page ?? 1;

            // Start with the base query
            IEnumerable<Post> items = _db.Posts.ToList();

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
        public IActionResult Add(Post model)
        {
            if (ModelState.IsValid)
            {
                return View(model);
            }

            model.CreateDate = DateTime.Now;
            model.ModifierDate = DateTime.Now;
            model.CategoryId = 1;
            model.Alias = WebsiteTMDT.Areas.Admin.Models.Common.Filter.FilterChar(model.Title);
            _db.Posts.Add(model);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            var item = _db.Posts.Find(id);
            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Post model)
        {
            if (ModelState.IsValid)
            {
                return View(model);
            }

            model.ModifierDate = DateTime.Now;
            model.Alias = WebsiteTMDT.Areas.Admin.Models.Common.Filter.FilterChar(model.Title);
            _db.Posts.Attach(model);
            _db.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var item = _db.Posts.Find(id);
            if (item != null)
            {
                _db.Posts.Remove(item);
                _db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
        //chỉnh hiện thị trong tin tức
        [HttpPost]
        public IActionResult IsActive(int id)
        {
            var item = _db.Posts.Find(id);
            if (item != null)
            {
                item.IsActive = !item.IsActive;
                _db.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _db.SaveChanges();
                return Json(new { success = true, isActive = item.IsActive });
            }
            return Json(new { success = false });
        }
        //Xóa tất cả mục đã chọn
        [HttpPost]
        public ActionResult DeleteAll(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var items = id.Split(",");
                if (items != null && items.Any())
                {
                    foreach (var item in items)
                    {
                        var obj = _db.Posts.Find(Convert.ToInt32(item));
                        _db.Posts.Remove(obj);
                        _db.SaveChanges();
                    }
                }
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
    }
}
