using System;
using System.Collections.Generic;
using System.Text;

namespace EMS.RegxConsts
{
    public class RegexConst
    {
        public const string NameRegex = @"^[a-zA-Z\s]{1,30}$";
        public const string PhoneNumberRegex = @"^[0-9]{10}$";
        public const string EmailRegex = @"^([a-zA-Z0-9_.-])+@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$";
    }
}
