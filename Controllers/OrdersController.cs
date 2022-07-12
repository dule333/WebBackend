using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebBackend.Dto;
using WebBackend.Infrastructure;
using WebBackend.Interfaces;

namespace WebBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("Customer/{id}")]
        public IActionResult GetOrders(int id)
        {
            return Ok(_orderService.GetOrders(id));
        }
        [HttpGet("Admin/{id}")]
        public IActionResult GetOrdersAdmin(int id)
        {
            return Ok(_orderService.GetOrdersAdmin(id));
        }
        [HttpGet("Postal/{id}")]
        public IActionResult GetOrdersPostal(int id)
        {
            return Ok(_orderService.GetOrdersPostal(id));
        }
        [HttpGet("PostalH/{id}")]
        public IActionResult GetOrdersPostalH(int id)
        {
            return Ok(_orderService.GetOrdersPostalHistory(id));
        }

        [HttpGet("{id}")]
        public IActionResult GetOrder(int id)
        {
            return Ok(_orderService.GetOrder(id));
        }

        [HttpPost]
        public IActionResult PostOrderDto([FromBody]OrderDto orderDto, int userId)
        {
            return Ok(_orderService.CreateOrder(orderDto, userId));
        }
        [HttpPost("Reserve/{postal}/{order}")]
        public IActionResult ReserveOrder(int postal, int order)
        {
            return Ok(_orderService.ReserveOrder(postal, order));
        }
    }
}
