using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Data.Contexts;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private AppDbContext Context;

        public DepartmentRepository(AppDbContext Context)
        {
            // Inject
            this.Context = Context;
        }

        public bool CheckUnique(string Name)
        {
            Department D = Context.Departments.FirstOrDefault(x => x.Name == Name);

            if(D == null)
            {
                return true;
            }

            return false;
        }

        public void Delete(int id)
        {
            Department D = Context.Departments.FirstOrDefault(x => x.DepartmentId == id);
            Context.Departments.Remove(D);

            Context.SaveChanges();
        }

        public void Edit(int id, Department Item)
        {
            Department D = Context.Departments.FirstOrDefault(x => x.DepartmentId == id);

            D.Code = Item.Code;
            D.Name = Item.Name;
            D.DateOfCreation = Item.DateOfCreation;

            Context.SaveChanges();
        }

        public List<Department> GetAll()
        {
            return Context.Departments.ToList();
        }

        public Department GetById(int id)
        {
            return Context.Departments.FirstOrDefault(x => x.DepartmentId == id);
        }

        public void Insert(Department Item)
        {
            Context.Departments.Add(Item);
        }
    }
}
