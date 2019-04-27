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
    public class UserServicesController : ControllerBase
    {
        readonly UserServiceRepository _userServiceRepository;

        public UserServicesController()
        {
            _userServiceRepository = new UserServiceRepository();
        }

        [HttpPost()]
        public ActionResult AddUserInterest(CreateUserServiceRequest createRequest)
        {

            var newUserService = _userServiceRepository.AddUserService(createRequest.UserId, createRequest.ServiceId);

            return Created($"api/userservices/{newUserService.Id}", newUserService);
        }

        [HttpGet]
        public ActionResult GetAllUserServices()
        {
            var userServices = _userServiceRepository.GetAll();

            return Ok(userServices);
        }
    }
}