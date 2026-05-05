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

        public DbSet<Customer> Customers { get; set; }
        public DbSet<ProductList> ProductLists { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        

        // Các bảng mới cho bài Quản lý kho thiết bị điện tử
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ImportTicket> ImportTickets { get; set; }
        public DbSet<ImportDetail> ImportDetails { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<ExportTicket> ExportTickets { get; set; }
        public DbSet<ExportDetail> ExportDetails { get; set; }

        
    }
}
