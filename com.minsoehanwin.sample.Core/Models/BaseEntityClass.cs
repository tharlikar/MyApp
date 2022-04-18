using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.minsoehanwin.sample.Core.Models
{
    public abstract class BaseEntityClass : AuditableEntity
    {
        public virtual int Id { get; set; }
    }
}
