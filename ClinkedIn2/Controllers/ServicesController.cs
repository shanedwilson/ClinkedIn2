﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClinkedIn2.Data;
using ClinkedIn2.Models;
using ClinkedIn2.Validators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClinkedIn2.Controllers
{
    [Route("api/[controller]"), ApiController]
    public class ServicesController : ControllerBase

    {
        readonly ServiceRepository _serviceRepository;
        readonly CreateServiceRequestValidator _validator;

        public ServicesController()
        {
            _validator = new CreateServiceRequestValidator();
            _serviceRepository = new ServiceRepository();
        }

        [HttpPost()]
        public ActionResult AddService(CreateServiceRequest createRequest)
        {
            if (!_validator.Validate(createRequest))
            {
                return BadRequest(new { error = "services must have a name" });
            }

            var newService = _serviceRepository.AddService(createRequest.Name, createRequest.Description, createRequest.Price);

            return Created($"api/services/{newService.Id}", newService);
        }

        [HttpGet]
        public ActionResult GetAllServices()
        {
            var services = _serviceRepository.GetAll();

            return Ok(services);
        }

        [HttpDelete("{userId}")]
        public ActionResult DeleteService(int userId)
        {
            _serviceRepository.DeleteService(userId);

            return Ok();
        }

        [HttpPut("{userId}")]
        public ActionResult UpdateService(int userId, UpdateServiceRequest updateServiceRequest)
        {
            if (updateServiceRequest == null)
            {
                return BadRequest(new { error = "Please provide necessary information" });
            }
            var updatedUser = _serviceRepository.UpdateService(
                userId,
                updateServiceRequest.Name,
                updateServiceRequest.Description,
                updateServiceRequest.Price);

            return Ok();
        }
    }
}