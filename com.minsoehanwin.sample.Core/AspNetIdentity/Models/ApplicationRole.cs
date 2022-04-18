using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.minsoehanwin.sample.Core.AspNetIdentity.Models
{

    public class ApplicationRole : IdentityRole
    {
        public string Description { get; set; }
        public ApplicationRole() { }
        public ApplicationRole(string name, string description)
            : base(name)
        {
            Description = description;
        }
    }

}
