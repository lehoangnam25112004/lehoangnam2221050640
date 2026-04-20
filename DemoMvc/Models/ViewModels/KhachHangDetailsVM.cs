namespace DemoMvc.Models.ViewModels;
public class KhachHangDetailsVM {
    public string TenKH { get; set; } = "";
    public List<DonInfo> DanhSachDon { get; set; } = new();
}

public class DonInfo {
    public int MaDH { get; set; }
    public DateTime NgayDat { get; set; }
    public decimal TongTien { get; set; }
    public List<string> TenSanPhams { get; set; } = new();
}