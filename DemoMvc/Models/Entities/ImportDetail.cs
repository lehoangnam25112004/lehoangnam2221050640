using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace DemoMvc.Models.Entities
{
    public class ImportDetail
    {
        [Key]
        public int Id { get; set; }

        public int ImportTicketId { get; set; }
        [ForeignKey("ImportTicketId")]
        public virtual ImportTicket? ImportTicket { get; set; }

        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product? Product { get; set; }

        [Display(Name = "Số lượng nhập")]
        public int Quantity { get; set; }

        [Display(Name = "Đơn giá nhập")]
        public decimal UnitPrice { get; set; }

        [NotMapped] // Không tạo cột này trong DB, chỉ dùng để tính toán nhanh trên giao diện
        public decimal SubTotal => Quantity * UnitPrice;
    }
}