using ECommerce.Payment.Domain.Entity;
using ECommerce.Payment.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Payment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost]
        public IActionResult Post([FromBody] PaymentEntity paymentEntity)
        {
            _paymentService.ProccessPayment(paymentEntity);
            return Ok();
        }
    }
}
