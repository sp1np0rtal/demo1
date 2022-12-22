using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DemoMVCCore.Utilities
{
    public class ValidEmailDomainAttribute : ValidationAttribute
    {

        private readonly string _allowedDomain;

        public ValidEmailDomainAttribute(string allowedDomain)
        {
            
            this._allowedDomain = allowedDomain;
        }

        public string AllowedDomain { get; }

        public override bool IsValid(object value)
        {
            string[] strings = value.ToString().Split("@");
            return (strings[1].Equals(_allowedDomain, StringComparison.OrdinalIgnoreCase));
            
        }
    }
}
