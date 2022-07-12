using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using WebBackend.Dto;
using WebBackend.Infrastructure;
using WebBackend.Interfaces;
using WebBackend.Models;

namespace WebBackend.Services
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly DeliveryContext _deliveryContext;

        public ProductService(IMapper mapper, DeliveryContext deliveryContext)
        {
            _mapper = mapper;
            _deliveryContext = deliveryContext;
        }

        public ProductDto AddProduct(ProductDto newProduct)
        {
            Product product = _mapper.Map<Product>(newProduct);
            _deliveryContext.Products.Add(product);
            _deliveryContext.SaveChanges();

            return _mapper.Map<ProductDto>(product);
        }

        public ProductDto GetProduct(int id)
        {
            return _mapper.Map<ProductDto>(_deliveryContext.Products.Find(id));
        }

        public List<ProductDto> GetProducts()
        {
            return _mapper.Map<List<ProductDto>>(_deliveryContext.Products.ToList());
        }
    }
}
