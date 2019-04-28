using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinkedIn2.Models
{
    public class UserService
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ServiceId { get; set; }

        public UserService(int userId, int serviceId)
        {
            UserId = userId;
            ServiceId = serviceId;
        }

        public UserService(int id, int userId, int serviceId)
        {
            Id = id;
            UserId = userId;
            ServiceId = serviceId;
        }
    }
}
