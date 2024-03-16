using EmployeeDemoApp.Models;
using System.ComponentModel.DataAnnotations;

namespace EmployeeDemoApp.ViewModels
{
    public class DepartmentViewModel
    {
        public int DepartmentId { get; set; }

        [Required(ErrorMessage ="Name is required")]
        public string Name { get; set; }
        
    }
}
