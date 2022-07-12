using System.Collections.Generic;

namespace WebBackend.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double UnitPrice { get; set; }
        public string Ingredients { get; set; }
        public List<OrderProduct> OrderProducts { get; set; }
    }
}
