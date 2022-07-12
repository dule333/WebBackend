﻿using System.Collections.Generic;
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
        [NotMapped]
        public Dictionary<int,int> products { get; set; }
        public DeliveryStatus DeliveryStatus { get; set; }
        public DateTime EndTime { get; set; }
    }
}
