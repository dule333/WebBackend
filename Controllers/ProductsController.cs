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
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: api/Products
        [HttpGet]
        [Authorize(Roles = "customer")]
        public IActionResult GetProducts()
        {
            return Ok(_productService.GetProducts());
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        [Authorize(Roles = "customer")]
        public IActionResult GetProduct(int id)
        {
            return Ok(_productService.GetProduct(id));
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult PostProduct(ProductDto productDto)
        {
            return Ok(_productService.AddProduct(productDto));
        }
    }
}
