using log4net;
using com.minsoehanwin.sample.Core.Models;
using com.minsoehanwin.sample.Services.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.minsoehanwin.sample.Services
{
    public class PassportInfoValidator:BaseValidator
    {
        public void Validate(PassportInfo passportInfo)
        {
            ValidatePassportNo(passportInfo.PassportNo);
        }

        public void ValidatePassportNo(string passportNo)
        {
            var error=0;
            var msg=string.Empty;
            if (string.IsNullOrEmpty(passportNo))
            {
                error++;
                msg += "PassportNo is null or empty.";
            }

            if (error > 0)
            {
                throw new InvalidPassportNoException(msg);
            }
        }

    }
}
