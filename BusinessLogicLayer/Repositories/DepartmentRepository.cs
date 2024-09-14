using DataAccessLayer.Data.Contexts;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Repositories
{
    public class DepartmentRepository
    {
        public IEnumerable<Department> GetAll()
        {
            using(AppDbContext Context = new AppDbContext())
            {
                return Context.Departments.ToList();
            }
        }

        public Department GetDept(int id)
        {
            using(AppDbContext Context = new AppDbContext())
            {
                return Context.Departments.FirstOrDefault(x => x.DepartmentId == id);
            }
        }

        public int Save(Department D)
        {
            using(AppDbContext Context = new AppDbContext())
            {
                Context.Departments.Add(D);

                return Context.SaveChanges();
            }
        }

        public int SaveUpdated(Department D, int id)
        {
            using (AppDbContext Context = new AppDbContext())
            {
                Department Dept = Context.Departments.FirstOrDefault(x => x.DepartmentId == id);

                Dept.Code = D.Code;
                Dept.Name = D.Name;
                Dept.DateOfCreation = D.DateOfCreation;

                return Context.SaveChanges();
            }
        }

        public int Delete(int id)
        {
            using(AppDbContext Context = new AppDbContext())
            {
                Department Dept = Context.Departments.FirstOrDefault(x => x.DepartmentId == id);
                Context.Departments.Remove(Dept);

                return Context.SaveChanges();
            }
        }

        public bool CheckUnique(string Name)
        {
            Department D;

            using(AppDbContext Context = new AppDbContext())
            {
                D = Context.Departments.FirstOrDefault(x => x.Name == Name);
            }

            if(D == null)
            {
                return true;
            }

            return false;
        }
    }
}
