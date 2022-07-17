﻿using ECommerce.Gateway.Models;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Gateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoneController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post(PaymentRequest paymentRequest)
        {
            return Ok(new PaymentResponse(paymentRequest, GatewayEnum.Stone));
        }

    }
}
