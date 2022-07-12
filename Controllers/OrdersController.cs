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

        // GET: api/Orders
        [HttpGet]
        public IActionResult GetOrderDto()
        {
            throw new NotImplementedException();
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public IActionResult GetOrderDto(int id)
        {

            throw new NotImplementedException();
        }

        // POST: api/Orders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public IActionResult PostOrderDto(OrderDto orderDto)
        {

            throw new NotImplementedException();
        }
    }
}
