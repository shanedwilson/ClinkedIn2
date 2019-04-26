using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinkedIn2.Models
{
    public class CreateUserServiceRequest
    {
        public int UserId { get; set; }
        public int ServiceId { get; set; }
    }
}
