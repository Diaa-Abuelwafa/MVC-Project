using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Repositories
{
    public class UnitOfWorkRepository : IUnitOfWorkRepository
    {
        public IEmployeeRepository EmployeeRepository { get; set; }
        public IDepartmentRepository DepartmentRepository { get; set; }
        public AppDbContext Context { get; set; }
        public UnitOfWorkRepository(AppDbContext Context)
        {
            this.Context = Context;

            EmployeeRepository = new EmployeeRepository(Context);
            DepartmentRepository = new DepartmentRepository(Context);
        }
    }
}
