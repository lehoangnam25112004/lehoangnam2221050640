using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace DemoMvc.Models.Entities
{
public class SanPham {
    [Key]
    public int MaSP { get; set; }
    [Required]
    public string? TenSP { get; set; }
    
    [Column(TypeName = "decimal(18,2)")]
    public decimal Gia { get; set; }
    
}
}