using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebsiteTMDT.Areas.Admin.Models.EF
{
    [Table("tb_Contract")]
    public class Contract : CommonAbstract
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Tên không được để trống")]
        [StringLength(150, ErrorMessage = "Quá 150 ký tự")]
        public string Name { get; set; }
        [StringLength(150, ErrorMessage = "Quá 150 ký tự")]
        public string? Website { get; set; }
        public string Email { get; set; }
        [StringLength(4000, ErrorMessage = "Quá 4000 ký tự")]
        public string Message { get; set; }
        public bool IsRead { get; set; }
    }
}
