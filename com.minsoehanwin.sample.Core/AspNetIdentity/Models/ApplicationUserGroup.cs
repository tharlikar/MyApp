using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.minsoehanwin.sample.Core.AspNetIdentity.Models
{

    public class ApplicationUserGroup
    {
        public virtual string UserId { get; set; }
        public virtual string GroupId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual Group Group { get; set; }
    }

}
