using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface IEmployeeRepository
    {
        public List<Employee> GetAll();
        public Employee GetById(int id);
        public void Insert(Employee Item);
        public void Edit(int id, Employee Item);
        public void Delete(int id);
    }
}
