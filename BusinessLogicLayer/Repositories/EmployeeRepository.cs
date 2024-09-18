using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Data.Contexts;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext Context;

        public EmployeeRepository(AppDbContext Context)
        {
            // Inject
            this.Context = Context;
        }
        public void Delete(int id)
        {
            Employee E = Context.Employees.FirstOrDefault(x => x.EmployeeId == id);

            Context.Employees.Remove(E);

            //Context.SaveChanges();
        }

        public void Edit(int id, Employee Item)
        {
            Employee E = Context.Employees.FirstOrDefault(x => x.EmployeeId == id);

            E.Name = Item.Name;
            E.Age = Item.Age;
            E.Address = Item.Address;
            E.Email = Item.Email;
            E.Salary = Item.Salary;
            E.PhoneNumber = Item.PhoneNumber;
            E.IsActive = Item.IsActive;
            E.HireDate = Item.HireDate;
            E.DeptId = Item.DeptId;

            //Context.SaveChanges();
        }

        public List<Employee> GetAll()
        {
            return Context.Employees.Include(x => x.Department).ToList();
        }

        public List<Employee> GetAllByName(string Word)
        {
            return Context.Employees.Include(x => x.Department).Where(x => x.Name.ToLower().Contains(Word.ToLower()) == true).ToList();
        }

        public Employee GetById(int id)
        {
            return Context.Employees.Include(x => x.Department).FirstOrDefault(x => x.EmployeeId == id);           
        }

        public void Insert(Employee Item)
        {
            Context.Employees.Add(Item);

            //Context.SaveChanges();
        }
        public int SaveChanges()
        {
            return Context.SaveChanges();
        }
    }
}
