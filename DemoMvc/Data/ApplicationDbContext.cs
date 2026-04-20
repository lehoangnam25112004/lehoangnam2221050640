using Microsoft.EntityFrameworkCore;
using DemoMvc.Models.Entities;

namespace DemoMvc.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Students { get; set; } = default !;  

        public DbSet<Class> Classes { get; set; }

        public DbSet<KhachHang> KhachHangs { get; set; }
        public DbSet<SanPham> SanPhams { get; set; }
        public DbSet<DonHang> DonHangs { get; set; }
        public DbSet<ChiTietDonHang> ChiTietDonHangs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);

    // 1. Liên kết Khách hàng - Đơn hàng (1-N)
    modelBuilder.Entity<DonHang>()
        .HasOne(d => d.KhachHang)
        .WithMany(k => k.DonHangs)
        .HasForeignKey(d => d.MaKH);

    // 2. Liên kết Đơn hàng - Chi tiết đơn hàng (1-N)
    modelBuilder.Entity<ChiTietDonHang>()
        .HasOne(ct => ct.DonHang)
        .WithMany(d => d.ChiTietDonHangs)
        .HasForeignKey(ct => ct.MaDH);

    // 3. Liên kết Sản phẩm - Chi tiết đơn hàng (1-N)
    modelBuilder.Entity<ChiTietDonHang>()
        .HasOne(ct => ct.SanPham)
        .WithMany() // Nếu bên SanPham không tạo ICollection thì để trống WithMany
        .HasForeignKey(ct => ct.MaSP);
}
    }
}
