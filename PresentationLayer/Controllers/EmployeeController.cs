using AutoMapper;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Controllers.Helpers;
using PresentationLayer.Models.ViewModels;

namespace PresentationLayer.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        //private readonly IEmployeeRepository EmployeeRepository;
        //private readonly IDepartmentRepository DepartmentRepository;
        //private readonly IMapper mapper;

        private readonly IUnitOfWorkRepository UnitOfWorkRepository;
        private readonly FileHelper Helper;

        public EmployeeController(IUnitOfWorkRepository UnitOfWorkRepository /*,IMapper Mapper*/,
                                  FileHelper Helper)
        {
            // Injection
            this.UnitOfWorkRepository = UnitOfWorkRepository;
            this.Helper = Helper;
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
        public IActionResult Add(Employee Emp, IFormFile? Image)
        {
            if (ModelState.IsValid == true)
            {
                if (Emp.DeptId != -1)
                {
                    if(Image != null)
                    {
                        FileViewModel FileModel = new FileViewModel();

                        FileModel = Helper.StoreFile(Image, "Images");

                        Emp.ImageNameWithGuid = FileModel.FileNameWithGuid;
                        Emp.ImageName = FileModel.FileName;
                    }

                    UnitOfWorkRepository.EmployeeRepository.Insert(Emp);
                    UnitOfWorkRepository.EmployeeRepository.SaveChanges();

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
        public IActionResult Update(int id, Employee Emp, IFormFile Image)
        {
            if (ModelState.IsValid == true)
            {
                if (Emp.DeptId != -1)
                {
                    if(Image != null)
                    {
                        Employee EmpBefore = UnitOfWorkRepository.EmployeeRepository.GetById(id);

                        if(EmpBefore.ImageName != Image.FileName)
                        {
                            if(EmpBefore.ImageNameWithGuid != null)
                            {
                                Helper.DeleteFile(EmpBefore.ImageNameWithGuid, "Images");
                            }

                            FileViewModel FileModel = Helper.StoreFile(Image, "Images");
                            Emp.ImageName = FileModel.FileName;
                            Emp.ImageNameWithGuid = FileModel.FileNameWithGuid;
                        }
                    }

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
            Employee Emp = UnitOfWorkRepository.EmployeeRepository.GetById(id);
            if(Emp.ImageNameWithGuid != null)
            {
                Helper.DeleteFile(Emp.ImageNameWithGuid, "Images");
            }

            UnitOfWorkRepository.EmployeeRepository.Delete(id);
            UnitOfWorkRepository.EmployeeRepository.SaveChanges();

            return RedirectToAction("Index");
        }
    } 
}
