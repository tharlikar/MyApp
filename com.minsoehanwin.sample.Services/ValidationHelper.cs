using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;
namespace com.minsoehanwin.sample.Services
{
    public class ValidationHelper
    {
        /// <summary>
        /// https://msdn.microsoft.com/en-us/library/01escwtf(v=vs.100).aspx
        /// </summary>
        public class RegexUtilities
        {
            bool invalid = false;

            public bool IsValidEmail(string strIn)
            {
                invalid = false;
                if (String.IsNullOrEmpty(strIn))
                    return false;

                // Use IdnMapping class to convert Unicode domain names.
                strIn = Regex.Replace(strIn, @"(@)(.+)$", this.DomainMapper);
                if (invalid)
                    return false;

                // Return true if strIn is in valid e-mail format.
                return Regex.IsMatch(strIn,
                       @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                       @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$",
                       RegexOptions.IgnoreCase);
            }

            private string DomainMapper(Match match)
            {
                // IdnMapping class with default property values.
                IdnMapping idn = new IdnMapping();

                string domainName = match.Groups[2].Value;
                try
                {
                    domainName = idn.GetAscii(domainName);
                }
                catch (ArgumentException)
                {
                    invalid = true;
                }
                return match.Groups[1].Value + domainName;
            }
        }
    }
}
