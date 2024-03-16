using EmployeeDemoApp.Data;
using EmployeeDemoApp.Models;
using EmployeeDemoApp.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace EmployeeDemoApp.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly AppDbContext db;
        public DepartmentRepository(AppDbContext db) {
            this.db = db;
        }
        public async Task AddAsync(DepartmentViewModel model)
        {
            var newdepartment = new Department()
            {

                Name = model.Name
            };

            await db.Departments.AddAsync(newdepartment);
            await db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
          Department department= await db.Departments.FindAsync(id);
            db.Departments.Remove(department);
            await db.SaveChangesAsync();
        }

        public async Task<List<DepartmentViewModel>> GetAllAsync()
        {
            List<Department> departments= await db.Departments.ToListAsync();
            List<DepartmentViewModel> departmentViewModels= new List<DepartmentViewModel>();
            foreach (var department in departments)
            {
                var departmentViewModel = new DepartmentViewModel()
                {
                    DepartmentId=department.DepartmentId,
                    Name = department.Name,
                };
                departmentViewModels.Add(departmentViewModel);
            }
            return departmentViewModels;
        }

        public async Task<DepartmentViewModel> GetByIdAsync(int id)
        {
            var department= await db.Departments.FindAsync(id);
            var departmentViewModel = new DepartmentViewModel()
            {
                DepartmentId = department.DepartmentId,
                Name = department.Name,
            };
            return departmentViewModel;
        }

        public async Task UpdateAsync(DepartmentViewModel departmentUpdated)
        {
           var department= await db.Departments.FindAsync(departmentUpdated.DepartmentId);

            department.Name = departmentUpdated.Name;

            db.Departments.Update(department);
            await db.SaveChangesAsync();

           
            
        }
    }
}
