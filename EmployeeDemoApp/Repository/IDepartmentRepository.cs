using EmployeeDemoApp.Models;
using EmployeeDemoApp.ViewModels;
using System.Threading.Tasks;

namespace EmployeeDemoApp.Repository
{
    public interface IDepartmentRepository
    {
        Task<DepartmentViewModel> GetByIdAsync(int id);
        Task<List<DepartmentViewModel>>  GetAllAsync();
        
        Task AddAsync(DepartmentViewModel department);
        Task UpdateAsync(DepartmentViewModel department);
        Task DeleteAsync(int id);
    }
}
