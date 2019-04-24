using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinkedIn2.Validators
{
    public class CreateUserRequestValidator
    {
        public bool Validate(CreateUserRequest requestToValidate)
        {
            return !(string.IsNullOrEmpty(requestToValidate.Name);
                   
        }
    }
}
