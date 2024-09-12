using BusinessLogicLayer.Repositories;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers
{
    public class DepartmentController : Controller
    {
        public IActionResult Index()
        {
            DepartmentRepository DepartmentClass = new DepartmentRepository();
            var Departments = DepartmentClass.GetAll();

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
            if(D.Name != null && D.Code != null)
            {
                DepartmentRepository DepartmentClass = new DepartmentRepository();
                int Result = DepartmentClass.Save(D);
                return RedirectToAction("Index");
            }

            return View("AddDepartment", D);
        }

        public IActionResult Details(int id)
        {
            DepartmentRepository DepartmentClass = new DepartmentRepository();
            Department Dept = DepartmentClass.GetDept(id);

            return View("DepartmentDetails", Dept);
        }

        public IActionResult Update(int id)
        {
            DepartmentRepository DepartmentClass = new DepartmentRepository();
            Department Dept = DepartmentClass.GetDept(id);

            return View("UpdateDepartment", Dept);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveEdit(Department D, int id)
        {
            if(D.Name != null && D.Code != null)
            {
                DepartmentRepository DepartmentClass = new DepartmentRepository();
                int Result = DepartmentClass.SaveUpdated(D, id);

                return RedirectToAction("Index");
            }

            return View("UpdateDepartment", D);
        }

        public IActionResult Delete(int id)
        {
            DepartmentRepository DepartmentClass = new DepartmentRepository();
            Department Dept = DepartmentClass.GetDept(id);

            return View("DeleteDepartment", Dept);
        }

        public IActionResult SaveDelete (int id)
        {
            DepartmentRepository DepartmentClass = new DepartmentRepository();
            int Result = DepartmentClass.Delete(id);

            return RedirectToAction("Index");
        }
    }
}
