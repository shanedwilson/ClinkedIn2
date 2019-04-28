using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinkedIn2.Models
{
    public class UserInterest
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int InterestId { get; set; }

        public UserInterest(int userId, int interestId)
        {
            UserId = userId;
            InterestId = interestId;
        }

        public UserInterest(int id, int userId, int interestId)
        {
            Id = id;
            UserId = userId;
            InterestId = interestId;
        }
    }
}
