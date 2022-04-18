using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.minsoehanwin.sample.Core.AspNetIdentity.Models
{

    public class Group
    {
        public Group() { }


        public Group(string name)
            : this()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Roles = new List<ApplicationRoleGroup>();
            this.Name = name;
        }
        public virtual string Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual ICollection<ApplicationRoleGroup> Roles { get; set; }
    }

}
