using System.Collections.Generic;
using WebBackend.Models;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebBackend.Dto
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public double TotalPrice { get; set; }
        public string Address { get; set; }
        [NotMapped]
        public Dictionary<int,int> OrderProducts { get; set; }
        public DeliveryStatus DeliveryStatus { get; set; }
        public DateTime EndTime { get; set; }
    }
}
