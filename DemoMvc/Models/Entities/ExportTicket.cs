using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoMvc.Models.Entities
{
    public class ExportTicket
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Ngày xuất không được để trống")]
        [Display(Name = "Ngày xuất kho")]
        public DateTime ExportDate { get; set; }

        [Display(Name = "Người nhận hàng")]
        public string? ReceiverName { get; set; }

        [Display(Name = "Tổng tiền xuất")]
        public decimal TotalAmount { get; set; }

        // Liên kết với các chi tiết phiếu xuất (1-N)
        public ICollection<ExportDetail>? ExportDetails { get; set; }
    }
}