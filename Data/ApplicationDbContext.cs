using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebsiteTMDT.Areas.Admin.Models.EF;

namespace WebsiteTMDT.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public string FullName { get; set; }
        public string Phone { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Adv> Advs { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<New> News { get; set; }

        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Subcribe> Subcribes { get; set; }
        public DbSet<SystemSetting> SystemSettings { get; set; }
    }
}
