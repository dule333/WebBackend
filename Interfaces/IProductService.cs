using System.Collections.Generic;
using WebBackend.Dto;

namespace WebBackend.Interfaces
{
    public interface IProductService
    {
        List<ProductDto> GetProducts();
        ProductDto GetProduct(int id);
        ProductDto AddProduct(ProductDto newProduct);
    }
}
