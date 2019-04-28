using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClinkedIn2.Data;
using ClinkedIn2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClinkedIn2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserInterestsController : ControllerBase

    {
        readonly UserInterestRepository _userInterestRepository;

        public UserInterestsController()
        {
          _userInterestRepository = new UserInterestRepository();
        }

        [HttpPost()]
        public ActionResult AddUserInterest(CreateUserInterestRequest createRequest)
        {

            var newUserInterest = _userInterestRepository.AddUserInterest(createRequest.UserId, createRequest.InterestId);

            return Created($"api/userinterests/{newUserInterest.Id}", newUserInterest);
        }

        [HttpGet]
        public ActionResult GetAllUserInterests()
        {
            var userInterests = _userInterestRepository.GetAll();

            return Ok(userInterests);
        }

        [HttpDelete("{userId}")]
        public ActionResult DeleteUserInterest(int userId)
        {
            _userInterestRepository.DeleteUserInterest(userId);

            return Ok();
        }

        [HttpPut("{userId}")]
        public ActionResult UpdateUserInterest(int userId, UpdateUserInterestRequest updateUserInterestRequest)
        {
            if (updateUserInterestRequest == null)
            {
                return BadRequest(new { error = "Please provide necessary information" });
            }
            var updatedUser = _userInterestRepository.UpdateUserInterest(
                userId,
                updateUserInterestRequest.UserId,
                updateUserInterestRequest.InterestId);

            return Ok();
        }
    }
}