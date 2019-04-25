using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinkedIn2.Models
{
    public class Interest
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Interest (string name)
        {
            Name = name;
        }
    }
}
