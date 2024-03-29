using EmployeeDemoApp.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace EmployeeDemoApp.ViewModels
{
    public class EmployeeViewModel
    {
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "LastName is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "DOB is required")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "number is required")]
        [Phone(ErrorMessage = "Invalid number")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }

        public bool IsActive { get; set; }

      
        public string? Image { get; set; }

        [ForeignKey("Deaprtment")]
        public int DepartmentId { get; set; }
        public Department? Department { get; set; }
    }
}
