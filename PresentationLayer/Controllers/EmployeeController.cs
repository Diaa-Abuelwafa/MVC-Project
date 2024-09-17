using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository EmployeeRepository;
        private readonly IDepartmentRepository DepartmentRepository;

        public EmployeeController(IEmployeeRepository EmployeeRepository, IDepartmentRepository DepartmentRepository)
        {
            // Inject
            this.EmployeeRepository = EmployeeRepository;
            this.DepartmentRepository = DepartmentRepository;
        }
        public IActionResult Index()
        {
            List<Employee> Employees = EmployeeRepository.GetAll();

            return View(Employees);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(Employee Emp)
        {
            if (ModelState.IsValid == true)
            {
                if (Emp.DeptId != -1)
                {
                    EmployeeRepository.Insert(Emp);

                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("DeptId", "You Must Choose Department");
                }
            }

            return View("Add", Emp);
        }

        public IActionResult Details(int id)
        {
            Employee Emp = EmployeeRepository.GetById(id);

            return View(Emp);
        }

        public IActionResult Update(int id)
        {
            Employee Emp = EmployeeRepository.GetById(id);

            return View(Emp);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, Employee Emp)
        {
            if (ModelState.IsValid == true)
            {
                if (Emp.DeptId != -1)
                {
                    EmployeeRepository.Edit(id, Emp);

                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("DptId", "You Must Choose Department");
                }
            }

            return View("Update", Emp);
        }

        public IActionResult Delete(int id)
        {
            Employee Emp = EmployeeRepository.GetById(id);

            return View(Emp);
        }

        public IActionResult SaveDelete(int id)
        {
            EmployeeRepository.Delete(id);

            return RedirectToAction("Index");
        }
    } 
}
