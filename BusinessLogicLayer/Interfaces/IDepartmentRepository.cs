using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface IDepartmentRepository
    {
        public List<Department> GetAll();
        public Department GetById(int id);
        public void Insert(Department Item);
        public void Edit(int id, Department Item);
        public void Delete(int id);
        public bool CheckUnique(string Name);
    }
}
