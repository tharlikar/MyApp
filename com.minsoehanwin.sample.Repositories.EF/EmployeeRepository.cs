using System;
using System.Collections.Generic;
using com.minsoehanwin.sample.Core;
using com.minsoehanwin.sample.Core.Models;
using com.minsoehanwin.sample.Core.Repositories;
using System.Linq;
using log4net;

namespace com.minsoehanwin.sample.Repositories.EF
{
    public class EmployeeRepository: GenericRepository<Employee>,IEmployeeRepository
    {
        public EmployeeRepository(IUnitOfWork unitOfWork)
            :base(unitOfWork)
        {
        }

        public void Save(Employee employee)
        {
            AddOrUpdate(employee);
        }
    }
}