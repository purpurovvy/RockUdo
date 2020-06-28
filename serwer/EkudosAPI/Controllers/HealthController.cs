using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EkudosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class HealthController : ControllerBase
    {
        // GET: api/Health
        [HttpGet]
        public string Get()
        {
            return "Surprise";
        }

        // GET: api/Health/5
        [HttpGet("{securityWord}", Name = "Get")]
        public OkObjectResult Get(string securityWord)
        {
            if (securityWord != "RocKudo")
            {
                new BadRequestResult();
            }

            var result = "It's OK";
            return Ok(result);
        }
    }
}
