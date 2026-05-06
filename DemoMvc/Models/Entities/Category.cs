using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace DemoMvc.Models.Entities
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên loại không được để trống")]
        [Display(Name = "Tên loại thiết bị")]
        public string Name { get; set; } = null!;
        // Quan hệ: Một loại có nhiều thiết bị
        public virtual ICollection<Product>? Products { get; set; }
    }
}