using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.minsoehanwin.sample.Core.Models
{
    public abstract class AuditableEntity
    {
        public virtual Nullable<DateTime> CreatedDate { get; set; }
        public virtual string CreatedBy { get; set; }
        public virtual Nullable<DateTime> UpdatedDate { get; set; }
        public virtual string UpdatedBy { get; set; }
    }
}
