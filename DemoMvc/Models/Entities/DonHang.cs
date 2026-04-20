using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace DemoMvc.Models.Entities
{
public class DonHang {
    [Key]
    public int MaDH { get; set; }
    public DateTime NgayDat { get; set; }
    public int MaKH { get; set; } // Khóa ngoại
    public virtual KhachHang? KhachHang { get; set; }
    public virtual ICollection<ChiTietDonHang>? ChiTietDonHangs { get; set; }
}
}