using ClinkedIn2.Data;
using ClinkedIn2.Models;
using ClinkedIn2.Validators;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinkedIn2.Controllers
{
    [Route("api/[controller]"), ApiController]
    public class UsersController : ControllerBase

    {
        readonly UserRepository _userRepository;
        readonly CreateUserRequestValidator _validator;

        public UsersController()
        {
            _validator = new CreateUserRequestValidator();
            _userRepository = new UserRepository();
        }

        [HttpPost("register")]
        public ActionResult AddUser(CreateUserRequest createRequest)
        {
            if (!_validator.Validate(createRequest))
            {
                return BadRequest(new { error = "users must have a name" });
            }

            var newUser = _userRepository.AddUser(createRequest.Name, createRequest.ReleaseDate, createRequest.Age, createRequest.IsPrisoner);

            return Created($"api/users/{newUser.Id}", newUser);
        }

        [HttpGet]
        public ActionResult GetAllUsers()
        {
            var users = _userRepository.GetAll();

            return Ok(users);
        }

        [HttpDelete("{userId}")]
        public ActionResult DeleteUser(int userId)
        {
            _userRepository.DeleteUser(userId);

            return Ok();
        }

        [HttpPut("{userId}")]
        public ActionResult UpdateUser(int userId, UpdateUserRequest updateUserRequest)
        {
            if (updateUserRequest == null)
            {
                return BadRequest(new { error = "Please provide necessary information" });
            }
            var updatedUser = _userRepository.UpdateUser(
                userId,
                updateUserRequest.Name,
                updateUserRequest.ReleaseDate,
                updateUserRequest.Age,
                updateUserRequest.IsPrisoner);
            return Ok();
        }
    }
}
