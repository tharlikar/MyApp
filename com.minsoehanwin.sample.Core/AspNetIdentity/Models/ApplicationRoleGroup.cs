using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.minsoehanwin.sample.Core.AspNetIdentity.Models
{

    public class ApplicationRoleGroup
    {
        public virtual string RoleId { get; set; }
        public virtual string GroupId { get; set; }
        public virtual IdentityRole Role { get; set; }
        public virtual Group Group { get; set; }
    }

}
