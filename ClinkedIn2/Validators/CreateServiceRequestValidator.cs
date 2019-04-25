using ClinkedIn2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinkedIn2.Validators
{
    public class CreateServiceRequestValidator
    {
        public bool Validate(CreateServiceRequest requestToValidate)
        {
            return !(string.IsNullOrEmpty(requestToValidate.Name));
        }
    }
}
