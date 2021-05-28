using Fulfillment.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace FulfillmentApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _orderService;

        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        [Route("orders")]
        public IActionResult GetAllOrders()
        {
            try
            {
                var orders = _orderService.GetAllOrders();
                if (orders == null)
                    return NoContent();
                return Ok(orders);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

       
    }
}
