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

            var newUser = _userRepository.AddUser(createRequest.Name, createRequest.ReleaseDate, createRequest.Age, createRequest.IsPrisoner );

            return Created($"api/users/{newUser.Id}", newUser);
        }

        [HttpGet]
        public ActionResult GetAllUsers()
        {
            var users = _userRepository.GetAll();

            return Ok(users);
        }

        [HttpGet("interests")]
        public ActionResult GetUserWithInterests()
        {
            var users = _userRepository.GetUserWithInterests();

            return Ok(users);
        }

        [HttpGet("services")]
        public ActionResult GetUserWithServices()
        {
            var users = _userRepository.GetUserWithServices();

            return Ok(users);
        }
    }
}
