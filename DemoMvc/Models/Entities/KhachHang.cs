using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace DemoMvc.Models.Entities
{
public class KhachHang {
    [Key]
    public int MaKH { get; set; }
    [Required(ErrorMessage = "Tên là bắt buộc")]
    public string? TenKH { get; set; }
    public string? DienThoai { get; set; }
    public virtual ICollection<DonHang>? DonHangs { get; set; }
}
}