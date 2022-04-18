using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.minsoehanwin.sample.Services
{
    public class WifeValidator
    {
        public void Validate(IList<Core.Models.Wife> wifes)
        {
            foreach (var w in wifes)
            {
                Validate(w);
            }
        }

        private void Validate(Core.Models.Wife w)
        {
            ValidateFirstName(w.FirstName);
            ValidateLastName(w.LastName);
        }

        private void ValidateLastName(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                throw new com.minsoehanwin.sample.Services.Exception.EmployeeFirstNameException("Wife last name is null or empty.");
            }
        }

        private void ValidateFirstName(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                throw new com.minsoehanwin.sample.Services.Exception.EmployeeFirstNameException("Wife first name is null or empty.");
            }
        }
    }
}
