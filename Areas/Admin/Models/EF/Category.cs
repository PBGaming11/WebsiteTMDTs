using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebsiteTMDT.Areas.Admin.Models.EF
{
    [Table("tb_Category")]
    public class Category : CommonAbstract
    {
        public Category()
        {
            News = new HashSet<New>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên danh mục là bắt buộc.")]
        public string Title { get; set; }
        public string Alias { get; set; }

        public string? Description { get; set; }
        [StringLength(150)]
        public string? SeoTitle { get; set; }
        [StringLength(250)]
        public string? SeoDescription { get; set; }
        [StringLength(150)]
        public string? SeoKeywords { get; set; }
        public bool IsActive { get; set; }
        public int Position { get; set; }
        public ICollection<New> News { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}
