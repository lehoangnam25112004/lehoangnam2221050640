using System.ComponentModel.DataAnnotations;
namespace DemoMvc.Models.Entities
{
    public class Student
    {
        [Key]
        public string StudentCode { get; set; } = default !;
        public string FullName { get; set; } = default !;

        public int? Age { get; set; }
    }
}