﻿using Microsoft.AspNetCore.Mvc;

namespace DotnetCoreLogging.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {

        private readonly ILogger<CustomerController> _logger;
        public CustomerController(ILogger<CustomerController> logger)
        {
            _logger = logger;
        }

        public string Get()
        {
            _logger.Log(LogLevel.Information, "Get customer at " + DateTimeOffset.UtcNow.ToString());
            _logger.LogInformation("Get customer at "+ DateTimeOffset.UtcNow.ToString());
            return "Customer 1";
        }
    }
}
