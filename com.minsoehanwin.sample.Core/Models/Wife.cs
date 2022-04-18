using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.minsoehanwin.sample.Core.Models
{
    public class Wife:BaseEntityClass
    {
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }

        public virtual int? EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
