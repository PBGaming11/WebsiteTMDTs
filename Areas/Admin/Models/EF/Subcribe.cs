using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebsiteTMDT.Areas.Admin.Models.EF
{
    [Table("tb_Subcribe")]
    public class Subcribe
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Email { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
