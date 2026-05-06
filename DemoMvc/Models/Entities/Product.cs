using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace DemoMvc.Models.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Tên thiết bị")]
        public string? Name { get; set; }

        [Display(Name = "Số lượng tồn")]
        public int StockQuantity { get; set; } = 0;

        [Display(Name = "Giá bán")]
        public decimal Price { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public virtual Category? Category { get; set; }
    }
}