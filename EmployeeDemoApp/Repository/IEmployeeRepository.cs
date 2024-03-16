using EmployeeDemoApp.Models;
using EmployeeDemoApp.ViewModels;

namespace EmployeeDemoApp.Repository
{
    public interface IEmployeeRepository
    {
        Task<EmployeeViewModel>  GetByidAsync(int id);
        Task<List<EmployeeViewModel>> GetAllAsync();
        Task AddAsync(EmployeeViewModel employee,IFormFile image);
        Task DeleteAsync(int id);
        Task UpdateAsync(EmployeeViewModel employee,IFormFile image);
        Task<List<DepartmentViewModel>> getAllDepartments();
        Task<List<EmployeeViewModel>> empWithDepartment();
    }
}
