using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace DemoMvc.Models.Entities
{
    public class ImportTicket
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Ngày nhập kho")]
        public DateTime ImportDate { get; set; } = DateTime.Now;

        [Required]
        public int SupplierId { get; set; }
        
        [Display(Name = "Tổng tiền")]
        public decimal TotalAmount { get; set; }

        // Quan hệ: Một phiếu nhập có nhiều dòng chi tiết
        public virtual ICollection<ImportDetail> ImportDetails { get; set; } = new List<ImportDetail>();
    }
}