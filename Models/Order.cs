using System.Collections.Generic;

namespace WebBackend.Models
{
    public enum DeliveryStatus
    {
        Free,
        InProgress,
        Delivered
    }
    public class Order
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public double TotalPrice { get; set; }
        public User Customer { get; set; }
        public User Postal { get; set; }
        public ICollection<Product> Products { get; set; }
        public List<OrderProduct> OrderProducts { get; set; }
        public DeliveryStatus DeliveryStatus { get; set; }
    }
}
