using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace DemoMvc.Models.Entities
{
public class ChiTietDonHang {
    [Key]
    public int MaCT { get; set; }
    public int MaDH { get; set; }
    public int MaSP { get; set; }
    public int SoLuong { get; set; }
    public virtual DonHang? DonHang { get; set; }
    public virtual SanPham? SanPham { get; set; }
}
}