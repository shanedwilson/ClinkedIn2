using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinkedIn2.Models
{
    public class User
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int Age { get; set; }
        public bool IsPrisoner { get; set; }


        public User(int id, string name, int age, bool isPrisoner, DateTime releaseDate)
        {
            Id = id;
            Name = name;
            ReleaseDate = releaseDate;
            Age = age;
            IsPrisoner = isPrisoner;
        }
    }
}
