using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface IGenericRepository<T>
    {
        public List<T> GetAll();
        public T GetById(int id);
        public void Insert(T Item);
        public void Edit(int id, T Item);
        public void Delete(int id);
    }
}
