using System.ComponentModel.DataAnnotations;

namespace WebApp2ByUmesh.Models
{
    public class Student
    {
        public int StdID { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, ErrorMessage = "Name cannot exceed 50 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Faculty is required")]
        public string Faculty { get; set; }
    }
}