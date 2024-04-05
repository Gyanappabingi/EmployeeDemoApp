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
        private readonly IWebHostEnvironment webhost;
        public EmployeeController(IEmployeeRepository employeeRepository, IWebHostEnvironment webhost) { 
            this.employeeRepository = employeeRepository;
            this.webhost = webhost;
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
            TempData["AlertMessage"] = "Employee added successfully...";
            return RedirectToAction("Index", "Employee");
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var existingemployee = await employeeRepository.GetByidAsync(id);

            string uploadsFolder = Path.Combine(webhost.WebRootPath, "images");
            string oldfilePath = Path.Combine(uploadsFolder, existingemployee.Image);

            if (System.IO.File.Exists(oldfilePath))
            {
                System.IO.File.Delete(oldfilePath);
                
            }
           
            await employeeRepository.DeleteAsync(id);

            TempData["AlertMessage"] = "Employee deleted successfully...";
            return RedirectToAction("Index", "Employee");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var departments = await employeeRepository.getAllDepartments();
            
            var emp=await employeeRepository.GetByidAsync(id);
           
            emp.Image = "/images/" +emp.Image;
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
            TempData["AlertMessage"] = "Employee updated successfully...";

            return RedirectToAction("Index", "Employee");
        }

        
       
    }
}
