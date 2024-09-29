using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Repositories;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers
{
    public class DepartmentController : Controller
    {
        //IDepartmentRepository DepartmentRepository;
        private readonly IUnitOfWorkRepository UnitOfWorkRepository;
        public DepartmentController(IUnitOfWorkRepository UnitOfWorkRepository)
        {
            //this.DepartmentRepository = DepartmentRepository;
            this.UnitOfWorkRepository = UnitOfWorkRepository;
        }
        public IActionResult Index()
        {
            List<Department> Departments = UnitOfWorkRepository.DepartmentRepository.GetAll();

            return View("AllDepartments", Departments);
        }

        public IActionResult Add()
        {
            return View("AddDepartment");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Department D)
        {
            if(ModelState.IsValid == true)
            {
                UnitOfWorkRepository.DepartmentRepository.Insert(D);
                UnitOfWorkRepository.DepartmentRepository.SaveChanges();

                return RedirectToAction("Index");
            }

            return View("AddDepartment", D);
        }

        public IActionResult Details(int id)
        {
            Department Department = UnitOfWorkRepository.DepartmentRepository.GetById(id);

            return View("DepartmentDetails", Department);
        }

        public IActionResult Update(int id)
        {
            Department Department = UnitOfWorkRepository.DepartmentRepository.GetById(id);

            return View("UpdateDepartment", Department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveEdit(Department D, int id)
        {
            if(D.Name != null && D.Code != null)
            {
                UnitOfWorkRepository.DepartmentRepository.Edit(id, D);
                UnitOfWorkRepository.DepartmentRepository.SaveChanges();

                return RedirectToAction("Index");
            }

            return View("UpdateDepartment", D);
        }

        public IActionResult Delete(int id)
        {
            Department Department = UnitOfWorkRepository.DepartmentRepository.GetById(id);

            return View("DeleteDepartment", Department);
        }

        public IActionResult SaveDelete (int id)
        {
            UnitOfWorkRepository.DepartmentRepository.Delete(id);
            UnitOfWorkRepository.DepartmentRepository.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult CheckName(string Name)
        {
            bool Flag = UnitOfWorkRepository.DepartmentRepository.CheckUnique(Name);

            if(Flag)
            {
                return Json(true);
            }

            return Json(false);
        }

        // For Just Test
        [Route("emp/{Name:alpha}")]
        // OR : [HttpGet("emp/{Name:alpha}")]
        public IActionResult ActionRoute(string Name)
        {
            return Content($"This Name : {Name}");
        }
    }
}
