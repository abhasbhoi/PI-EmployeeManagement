using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagement.Models
{
    [Table("Employee")]
    public class Employee
    {
        public long Id { get; set; }

        [Required]
        [StringLength(20,ErrorMessage = "FirstName length cannot exceed 20")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "LastName length cannot exceed 20")]
        public string LastName { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Designation length cannot exceed 20")]
        public string Designation { get; set; }

        [Required]
        public decimal Salary { get; set; }
    }
}
