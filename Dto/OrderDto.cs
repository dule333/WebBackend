using System.Collections.Generic;
using WebBackend.Models;

namespace WebBackend.Dto
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public double TotalPrice { get; set; }
        public List<OrderProduct> OrderProducts { get; set; }
        public DeliveryStatus DeliveryStatus { get; set; }
    }
}
