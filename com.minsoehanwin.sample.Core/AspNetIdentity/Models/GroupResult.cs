using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.minsoehanwin.sample.Core.AspNetIdentity.Models
{


    public class GroupResult
    {
        public bool Succeeded { get; set; }
        public ICollection<string> Errors { get; set; }
    }

}
