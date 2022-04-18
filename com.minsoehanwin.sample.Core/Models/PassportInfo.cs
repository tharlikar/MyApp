using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.minsoehanwin.sample.Core.Models
{
    public class PassportInfo:AuditableEntity
    {
        public virtual int EmployeeId { get; set; }
        public virtual string PassportNo { get; set; }
        public virtual DateTime IssueDate { get; set; }
        public virtual DateTime ExpiredDate { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
