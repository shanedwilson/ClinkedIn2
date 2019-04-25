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
    public class InterestsController : ControllerBase

    {
        readonly InterestRepository _interestRepository;
        readonly CreateInterestRequestValidator _validator;

        public InterestsController()
        {
            _validator = new CreateInterestRequestValidator();
            _interestRepository = new InterestRepository();
        }

        [HttpPost()]
        public ActionResult AddInterest(CreateInterestRequest createRequest)
        {
            if (!_validator.Validate(createRequest))
            {
                return BadRequest(new { error = "interests must have a name" });
            }

            var newInterest = _interestRepository.AddInterest(createRequest.Name);

            return Created($"api/interests/{newInterest.Id}", newInterest);
        }

        [HttpGet]
        public ActionResult GetAllInterests()
        {
            var interests = _interestRepository.GetAll();

            return Ok(interests);
        }
    }
}
