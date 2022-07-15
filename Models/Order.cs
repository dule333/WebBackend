using System;
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
        public string Address { get; set; }
        public User Customer { get; set; }
        public int CustomerId { get; set; }
        public User Postal { get; set; }
        public int? PostalId { get; set; }
        public List<OrderProduct> OrderProducts { get; set; }
        public DeliveryStatus DeliveryStatus { get; set; }
        public DateTime EndTime { get; set; }
    }
}
