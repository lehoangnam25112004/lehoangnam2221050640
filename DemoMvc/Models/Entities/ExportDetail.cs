using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoMvc.Models.Entities
{
    public class ExportDetail
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ExportTicketId { get; set; }
        
        [ForeignKey("ExportTicketId")]
        public ExportTicket? ExportTicket { get; set; }

        [Required]
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Product? Product { get; set; }

        [Required(ErrorMessage = "Số lượng xuất là bắt buộc")]
        [Display(Name = "Số lượng")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Đơn giá xuất là bắt buộc")]
        [Display(Name = "Đơn giá xuất")]
        public decimal UnitPrice { get; set; }

        [NotMapped] // Không tạo cột này trong DB, chỉ dùng để tính toán nhanh trên giao diện
        public decimal SubTotal => Quantity * UnitPrice;
    }
}