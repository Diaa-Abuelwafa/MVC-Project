using AutoMapper;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers
{
    public class EmployeeController : Controller
    {
        //private readonly IEmployeeRepository EmployeeRepository;
        //private readonly IDepartmentRepository DepartmentRepository;
        //private readonly IMapper mapper;

        private readonly IUnitOfWorkRepository UnitOfWorkRepository;
        public EmployeeController(IUnitOfWorkRepository UnitOfWorkRepository /*,IMapper Mapper*/)
        {
            // Inject
            this.UnitOfWorkRepository = UnitOfWorkRepository;
            //mapper = Mapper;
        }
        public IActionResult Index(string Word)
        {
            List<Employee> Employees = new List<Employee>();

            if(Word == null)
            {
                Employees = UnitOfWorkRepository.EmployeeRepository.GetAll();
            }
            else
            {
                Employees = UnitOfWorkRepository.EmployeeRepository.GetAllByName(Word);
            }

            return View(Employees);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Departments = UnitOfWorkRepository.DepartmentRepository.GetAll();

            // Just Example For Practice
            //Department D = new Department();
            //var Result = mapper.Map<Employee>(D);

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
                    UnitOfWorkRepository.EmployeeRepository.Insert(Emp);

                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("DeptId", "You Must Choose Department");
                }
            }

            ViewBag.Departments = UnitOfWorkRepository.DepartmentRepository.GetAll();

            return View("Add", Emp);
        }

        public IActionResult Details(int id)
        {
            Employee Emp = UnitOfWorkRepository.EmployeeRepository.GetById(id);

            return View(Emp);
        }

        public IActionResult Update(int id)
        {
            Employee Emp = UnitOfWorkRepository.EmployeeRepository.GetById(id);

            ViewBag.Departments = UnitOfWorkRepository.DepartmentRepository.GetAll();

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
                    UnitOfWorkRepository.EmployeeRepository.Edit(id, Emp);
                    UnitOfWorkRepository.EmployeeRepository.SaveChanges();

                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("DptId", "You Must Choose Department");
                }
            }

            ViewBag.Departments = UnitOfWorkRepository.DepartmentRepository.GetAll();

            return View("Update", Emp);
        }

        public IActionResult Delete(int id)
        {
            Employee Emp = UnitOfWorkRepository.EmployeeRepository.GetById(id);

            return View(Emp);
        }

        public IActionResult SaveDelete(int id)
        {
            UnitOfWorkRepository.EmployeeRepository.Delete(id);
            UnitOfWorkRepository.EmployeeRepository.SaveChanges();

            return RedirectToAction("Index");
        }
    } 
}
