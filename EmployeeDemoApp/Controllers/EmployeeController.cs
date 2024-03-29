using EmployeeDemoApp.Models;
using EmployeeDemoApp.Repository;
using EmployeeDemoApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EmployeeDemoApp.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository employeeRepository;
       
        public EmployeeController(IEmployeeRepository employeeRepository) { 
            this.employeeRepository = employeeRepository;
          
        }
        public async Task<IActionResult> Index()
        {
          // var emps=await employeeRepository.GetAllAsync();
            var lists = await employeeRepository.empWithDepartment();
            return View(lists);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
             var departments=await employeeRepository.getAllDepartments();
            ViewBag.Departments = new SelectList(departments, "DepartmentId", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(EmployeeViewModel model, IFormFile image)
        {
            if (!ModelState.IsValid)
            {
                var departments = await employeeRepository.getAllDepartments();
                ViewBag.Departments = new SelectList(departments, "DepartmentId", "Name");
                return View(model);
            }
            await employeeRepository.AddAsync(model, image);
            return RedirectToAction("Index", "Employee");
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
           await employeeRepository.DeleteAsync(id);
            return RedirectToAction("Index", "Employee");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var departments = await employeeRepository.getAllDepartments();
            
            var emp=await employeeRepository.GetByidAsync(id);

            ViewBag.Departments = new SelectList(departments, "DepartmentId", "Name");

            
            return View(emp);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EmployeeViewModel model, IFormFile image,string oldimage)
        {

            if (!ModelState.IsValid)
            {
                var departments = await employeeRepository.getAllDepartments();

                ViewBag.Departments = new SelectList(departments, "DepartmentId", "Name");
                model.Image = oldimage;
                return View(model);

            }

            await employeeRepository.UpdateAsync(model, image);
            

            return RedirectToAction("Index", "Employee");
        }

        
       
    }
}
