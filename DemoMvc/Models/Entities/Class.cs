using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoMvc.Models.Entities
{
    public class Class
    {
        [Key] // Xác định đây là Khóa chính
        public int ClassId { get; set; }

        [Required(ErrorMessage = "Tên lớp không được để trống")]
        [Display(Name = "Tên lớp học")]
        public string ClassName { get; set; } = default!;

        // --- THÀNH PHẦN LIÊN KẾT ---
        
        // Navigation Property: Một lớp chứa một danh sách (Collection) các sinh viên
        // Dùng 'virtual' để hỗ trợ Lazy Loading trong Entity Framework
        public virtual ICollection<Student>? Students { get; set; }
    }
}