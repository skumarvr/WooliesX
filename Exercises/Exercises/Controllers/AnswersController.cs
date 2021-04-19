using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exercises.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnswersController : ControllerBase
    {
        private readonly ILogger<AnswersController> _logger;

        public AnswersController(ILogger<AnswersController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("User")]
        public UserTokenResponse GetUserToken()
        {
            return new UserTokenResponse();
        }
    }
}
