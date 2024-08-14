using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebsiteTMDT.Areas.Admin.Models.EF;
using WebsiteTMDT.Data;
using X.PagedList.Extensions;

namespace WebsiteTMDT.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ProductsController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index(string searchText, int? page)
        {
            var pageSize = 10;
            var pageIndex = page ?? 1;

            // Start with the base query
            IEnumerable<Product> items = _db.Products.ToList();

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
            ViewBag.ProductCategory = new SelectList(_db.ProductCategories.ToList(), "Id", "Title");
            return View();
        }
        public ActionResult Edit()
        {
            return View();
        }

    }
}
