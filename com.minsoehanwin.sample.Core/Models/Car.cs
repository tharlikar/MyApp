using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.minsoehanwin.sample.Core.Models
{
    public class Car : BaseEntityClass
    {
        public virtual string Name { get; set; }

        public virtual int? EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
