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
        public List<string> Interests { get; set; }
        public List<string> Services { get; set; }

        public User(int id, string name, DateTime releaseDate, int age, bool isPrisoner)
        {
            Id = id;
            Name = name;
            ReleaseDate = releaseDate;
            Age = age;
            IsPrisoner = isPrisoner;
        }

        public User(string name, DateTime releaseDate, int age, bool isPrisoner)
        {
            Name = name;
            ReleaseDate = releaseDate;
            Age = age;
            IsPrisoner = isPrisoner;
        }

        public User(string name, DateTime releaseDate, int age, bool isPrisoner, List<string> services, List<string> interests)
        {
            Name = name;
            ReleaseDate = releaseDate;
            Age = age;
            IsPrisoner = isPrisoner;
            Interests = interests;
            Services = services;
        }
    }
}
