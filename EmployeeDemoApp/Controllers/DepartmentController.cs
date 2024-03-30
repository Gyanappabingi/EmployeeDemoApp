using EmployeeDemoApp.Repository;
using EmployeeDemoApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeDemoApp.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;
        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }
        public async Task<IActionResult> Index()
        {
           var dep= await _departmentRepository.GetAllAsync();
            return View(dep);
        }

       [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(DepartmentViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            TempData["AlertMessage"] = "Department added successfully...";
           await _departmentRepository.AddAsync(model);
            return RedirectToAction("Index", "Department");
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
           await _departmentRepository.DeleteAsync(id);
            TempData["AlertMessage"] = "Department deleted successfully...";
            return RedirectToAction("Index", "Department");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
           var department= await _departmentRepository.GetByIdAsync(id);
            return View(department);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(DepartmentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

           await _departmentRepository.UpdateAsync(model);
            TempData["AlertMessage"] = "Department updated successfully...";
            return RedirectToAction("Index", "Department");
        }
    }
}
