using ECommerce.Order.Domain.Services;
using ECommerce.Order.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Order.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(OrderCreateDto orderCreateDto)
        {
            return Ok(await _orderService.CreateOrder(orderCreateDto));
        }



        [HttpPost("reproccess")]
        public async Task<IActionResult> Post(OrderReproccessDto orderReproccessDto)
        {
            return Ok(await _orderService.ReproccessOrder(orderReproccessDto));
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _orderService.GetAllOrders());
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var order = await _orderService.GetOrderById(id);
            return order != null ? Ok(order) : NotFound();
        }
    }
}
