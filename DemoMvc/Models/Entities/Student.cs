using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace DemoMvc.Models.Entities
{
    public class Student
    {
        [Key]
        [Required(ErrorMessage = "Mã sinh viên là bắt buộc")]
        [StringLength(10, ErrorMessage = "Mã sinh viên không được quá 10 ký tự")]
        public string StudentCode { get; set; } = default !;
        
        [Required(ErrorMessage = "Tên không được để trống")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Tên từ 3-50 ký tự")]
        public string FullName { get; set; } = default !;


        [Range(0, 150, ErrorMessage = "Tuổi phải là số từ 0 đến 150")]
        public int? Age { get; set; }

        // Thêm vào dưới dòng Age (dòng 17)
    public int ClassId { get; set; }

    [ForeignKey("ClassId")]
    public virtual Class? Class { get; set; }
    }
}