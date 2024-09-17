﻿using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface IDepartmentRepository : IGenericRepository<Department>
    {
        public bool CheckUnique(string Name);
    }
}
