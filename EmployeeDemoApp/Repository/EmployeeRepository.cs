using EmployeeDemoApp.Data;
using EmployeeDemoApp.Models;
using EmployeeDemoApp.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Transactions;
using static System.Net.Mime.MediaTypeNames;

namespace EmployeeDemoApp.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext db;
        private readonly IWebHostEnvironment webhost;
        public EmployeeRepository(AppDbContext db, IWebHostEnvironment webhost)
        {
            this.db = db;
            this.webhost=webhost;   
        }

        public async Task AddAsync(EmployeeViewModel employee,IFormFile image)
        {
            
            if (image != null && image.Length > 0)
            {
                   
                // Save image to server
                string uploadsFolder = Path.Combine(webhost.WebRootPath, "images");
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + image.FileName;
              
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                image.CopyTo(new FileStream(filePath, FileMode.Create));

                // Save image path to database
                //employee.Image = "/images/" + uniqueFileName;
                employee.Image = uniqueFileName;
            }

            var newEmployee = new Employee()
            {
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                DateOfBirth = employee.DateOfBirth,
                PhoneNumber = employee.PhoneNumber,
                Gender = employee.Gender,
                Email = employee.Email,
                Address = employee.Address,
                
                IsActive = employee.IsActive,
                Image = employee.Image,
                DepartmentId=employee.DepartmentId,
                
            };

           await db.Employees.AddAsync(newEmployee);
           await db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
          Employee employee= await db.Employees.FindAsync(id);
            db.Employees.Remove(employee);
            await db.SaveChangesAsync();
        }

        public async Task<List<EmployeeViewModel>> GetAllAsync()
        {
            List<Employee> employees= await db.Employees.ToListAsync();
            List<EmployeeViewModel> employeeViewModels= new List<EmployeeViewModel>();
            foreach (var employee in employees)
            {
                var employeeViewModel = new EmployeeViewModel()
                {
                    EmployeeId = employee.EmployeeId,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    DateOfBirth = employee.DateOfBirth,
                    PhoneNumber = employee.PhoneNumber,
                    Gender = employee.Gender,
                    Email = employee.Email,
                    Address = employee.Address,
                    Image=employee.Image,
                    IsActive = employee.IsActive,
                   
                };
                employeeViewModels.Add(employeeViewModel);
            }
            return employeeViewModels;
        }

        public async Task<List<DepartmentViewModel>> getAllDepartments()
        {
           List<Department> departments= await db.Departments.ToListAsync();
            List<DepartmentViewModel> departmentViewModels= new List<DepartmentViewModel>();
            foreach(var department in departments)
            {
                var departmentViewModel = new DepartmentViewModel()
                {
                    DepartmentId = department.DepartmentId,
                    Name = department.Name
                };
                departmentViewModels.Add(departmentViewModel);
            }
            return departmentViewModels;
        }

        public async Task<EmployeeViewModel> GetByidAsync(int id)
        {
            var employee = await db.Employees.FindAsync(id);
            var employeeViewModel = new EmployeeViewModel()
            {
                EmployeeId = employee.EmployeeId,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                DateOfBirth = employee.DateOfBirth,
                PhoneNumber = employee.PhoneNumber,
                Gender = employee.Gender,
                Email = employee.Email,
                Address = employee.Address,
                IsActive = employee.IsActive,
                Department=employee.Department,
                DepartmentId=employee.DepartmentId,
                Image= "/images/"+employee.Image, //employee.Image,
            };
            return employeeViewModel;
        }

        public async Task UpdateAsync(EmployeeViewModel employeeUpdated, IFormFile image)
        {
           

            
            var existingemployee = await db.Employees.FindAsync(employeeUpdated.EmployeeId);

            
            if (image != null && image.Length > 0)
            {
                // Save image to server
                string uploadsFolder = Path.Combine(webhost.WebRootPath, "images");
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + image.FileName;

                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                //delete old file path
                string oldfilePath = Path.Combine(uploadsFolder, existingemployee.Image);
                if (image != null)
                {
                    if (System.IO.File.Exists(oldfilePath))
                    {
                        System.IO.File.Delete(oldfilePath);
                    }
                    
                }

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await image.CopyToAsync(fileStream);
                }




                // Save image path to database
                employeeUpdated.Image = uniqueFileName;  



            }
            else
            {
                employeeUpdated.Image = existingemployee.Image;
            }
            existingemployee.FirstName=employeeUpdated.FirstName;
            existingemployee.LastName=employeeUpdated.LastName;
            existingemployee.Email=employeeUpdated.Email;
            existingemployee.DateOfBirth = employeeUpdated.DateOfBirth;
            existingemployee.PhoneNumber = employeeUpdated.PhoneNumber;
            existingemployee.Gender = employeeUpdated.Gender;
            existingemployee.Address = employeeUpdated.Address;
            existingemployee.IsActive = employeeUpdated.IsActive;
            existingemployee.Image = employeeUpdated.Image;
            existingemployee.DepartmentId=employeeUpdated.DepartmentId;
            //var newEmployee = new Employee()
            //{
            //    FirstName = employeeUpdated.FirstName,
            //    LastName = employeeUpdated.LastName,
            //    DateOfBirth = employeeUpdated.DateOfBirth,
            //    PhoneNumber = employeeUpdated.PhoneNumber,
            //    Gender = employeeUpdated.Gender,
            //    Email = employeeUpdated.Email,
            //    Address = employeeUpdated.Address,

            //    IsActive = employeeUpdated.IsActive,
            //    Image = employeeUpdated.Image,
            //    DepartmentId = employeeUpdated.DepartmentId,

            //};
            // db.Employees.Update(employee);
            db.Employees.Update(existingemployee);
            await db.SaveChangesAsync();
        }
        public async Task<List<EmployeeViewModel>> empWithDepartment()
        {
            List<Employee> employees = await db.Employees.Include(e => e.Department).ToListAsync();
           List<EmployeeViewModel> employeeViewModels=new List<EmployeeViewModel>();
           foreach(var employee in employees)
            {
                var viewModel = new EmployeeViewModel()
                {
                    EmployeeId = employee.EmployeeId,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    DateOfBirth = employee.DateOfBirth,
                    PhoneNumber = employee.PhoneNumber,
                    Gender = employee.Gender,
                    Email = employee.Email,
                    Address = employee.Address,
                    Department = employee.Department,
                    DepartmentId = employee.DepartmentId,
                    IsActive = employee.IsActive,
                   // Image =  employee.Image,
                     Image= "/images/" + employee.Image,
                };
                employeeViewModels.Add(viewModel);
            }
            return employeeViewModels;
           
        }
    }
}
