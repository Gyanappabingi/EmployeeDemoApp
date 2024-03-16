using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeDemoApp.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }
        public string Image { get; set; }

        [ForeignKey("Deaprtment")]
        public int DepartmentId { get; set; }
        public Department? Department { get; set; }
    }
}
