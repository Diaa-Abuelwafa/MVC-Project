using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Repositories;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers
{
    public class DepartmentController : Controller
    {
        IDepartmentRepository DepartmentRepository;

        public DepartmentController(IDepartmentRepository DepartmentRepository)
        {
            this.DepartmentRepository = DepartmentRepository;
        }
        public IActionResult Index()
        {
            List<Department> Departments = DepartmentRepository.GetAll();

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
                DepartmentRepository.Insert(D);
                return RedirectToAction("Index");
            }

            return View("AddDepartment", D);
        }

        public IActionResult Details(int id)
        {
            Department Department = DepartmentRepository.GetById(id);

            return View("DepartmentDetails", Department);
        }

        public IActionResult Update(int id)
        {
            Department Department = DepartmentRepository.GetById(id);

            return View("UpdateDepartment", Department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveEdit(Department D, int id)
        {
            if(D.Name != null && D.Code != null)
            {
                DepartmentRepository.Edit(id, D);

                return RedirectToAction("Index");
            }

            return View("UpdateDepartment", D);
        }

        public IActionResult Delete(int id)
        {
            Department Department = DepartmentRepository.GetById(id);

            return View("DeleteDepartment", Department);
        }

        public IActionResult SaveDelete (int id)
        {
            DepartmentRepository.Delete(id);

            return RedirectToAction("Index");
        }

        public IActionResult CheckName(string Name)
        {
            bool Flag = DepartmentRepository.CheckUnique(Name);

            if(Flag)
            {
                return Json(true);
            }

            return Json(false);
        }

        [Route("emp/{Name:alpha}")]
        // OR : [HttpGet("emp/{Name:alpha}")]
        public IActionResult ActionRoute(string Name)
        {
            return Content($"This Name : {Name}");
        }
    }
}
