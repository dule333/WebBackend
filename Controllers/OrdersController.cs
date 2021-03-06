using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "customer")]
        public IActionResult GetOrders(int id)
        {
            return Ok(_orderService.GetOrders(id));
        }
        [HttpGet("Admin")]
        [Authorize(Roles = "admin")]
        public IActionResult GetOrdersAdmin()
        {
            return Ok(_orderService.GetOrdersAdmin());
        }
        [HttpGet("Postal")]
        [Authorize(Roles = "postal")]
        public IActionResult GetOrdersPostal()
        {
            return Ok(_orderService.GetOrdersPostal());
        }
        [HttpGet("PostalH/{id}")]
        [Authorize(Roles = "postal")]
        public IActionResult GetOrdersPostalH(int id)
        {
            return Ok(_orderService.GetOrdersPostalHistory(id));
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "postal,customer")]
        public IActionResult GetOrder(int id)
        {
            return Ok(_orderService.GetOrder(id));
        }

        [HttpPost]
        [Authorize(Roles = "customer")]
        public IActionResult PostOrderDto([FromBody]OrderDto orderDto, int userId)
        {
            return Ok(_orderService.CreateOrder(orderDto, userId));
        }
        [HttpGet("Reserve/{postal}/{order}")]
        [Authorize(Roles = "postal")]
        public IActionResult ReserveOrder(int postal, int order)
        {
            return Ok(_orderService.ReserveOrder(order, postal));
        }
        [HttpGet("Delivered/{order}")]
        [Authorize(Roles = "customer,postal")]
        public IActionResult OrderDelivered(int order)
        {
            return Ok(_orderService.OrderDelivered(order));
        }
    }
}
