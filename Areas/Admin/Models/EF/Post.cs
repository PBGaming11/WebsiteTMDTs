using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace WebsiteTMDT.Areas.Admin.Models.EF
{
    [Table("tb_Post")]
    public class Post : CommonAbstract
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Không được để trống tiêu đề")]
        [StringLength(150)]
        public string Title { get; set; }
        public string Alias { get; set; }
        public int CategoryId { get; set; }
        public string? Description { get; set; }
        [AllowHtml]
        public string? Detail { get; set; }
        public string? Image { get; set; }
        [StringLength(250)]
        public string? SeoTitle { get; set; }
        [StringLength(500)]
        public string? SeoDescription { get; set; }
        [StringLength(250)]
        public string? SeoKeywords { get; set; }
        public bool IsActive { get; set; }
        public int Position { get; set; }
        public virtual Category Category { get; set; }
    }
}
