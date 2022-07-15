using AutoMapper;
using System;
using System.Collections.Generic;
using WebBackend.Dto;
using WebBackend.Infrastructure;
using WebBackend.Interfaces;
using WebBackend.Models;

namespace WebBackend.Services
{
    public class OrderService : IOrderService
    {
        private readonly IMapper _mapper;
        private readonly DeliveryContext _deliveryContext;

        public OrderService(IMapper mapper, DeliveryContext deliveryContext)
        {
            _mapper = mapper;
            _deliveryContext = deliveryContext;
        }

        public OrderDto CreateOrder(OrderDto order, int id)
        {
            Random random = new Random();
            Order newOrder = new Order
            {
                Id = order.Id,
                Comment = order.Comment,
                TotalPrice = 150,
                CustomerId = id,
                Customer = _deliveryContext.Users.Find(id),
                DeliveryStatus = DeliveryStatus.Free,
                EndTime = System.DateTime.Now.AddMinutes(random.Next(8,15)),
                Address = order.Address,
                OrderProducts = new List<OrderProduct>()
            };
            OrderProduct orderProduct;
            foreach (var item in order.OrderProducts)    
            {
                orderProduct = new OrderProduct
                {
                    OrderId = id,
                    Order = newOrder,
                    Product = _deliveryContext.Products.Find(item.Key),
                    ProductId = item.Key,
                    Quantity = item.Value
                };

                _deliveryContext.OrderProducts.Add(orderProduct);
                newOrder.TotalPrice += item.Value * _deliveryContext.Products.Find(item.Key).UnitPrice;
            }

            _deliveryContext.Orders.Add(newOrder);
            _deliveryContext.SaveChanges();
            order.Id = newOrder.Id;
            return _mapper.Map<OrderDto>(order);
        }

        public OrderDto GetOrder(int orderId)
        {
            return _mapper.Map<OrderDto>(_deliveryContext.Orders.Find(orderId));
        }

        public List<OrderDto> GetOrders(int id)
        {
            List<Order> orders = _mapper.Map<List<Order>>(_deliveryContext.Orders);
            return _mapper.Map<List<OrderDto>>(orders.FindAll(x => x.CustomerId == id && x.DeliveryStatus == DeliveryStatus.Delivered));
        }

        public List<OrderDto> GetOrdersAdmin()
        {
            return _mapper.Map<List<OrderDto>>(_deliveryContext.Orders);
        }

        public List<OrderDto> GetOrdersPostal()
        {
            List<Order> orders = _mapper.Map<List<Order>>(_deliveryContext.Orders);
            return _mapper.Map<List<OrderDto>>(orders.FindAll(x => x.DeliveryStatus == DeliveryStatus.Free));

        }

        public List<OrderDto> GetOrdersPostalHistory(int id)
        {
            List<Order> orders = _mapper.Map<List<Order>>(_deliveryContext.Orders);
            return _mapper.Map<List<OrderDto>>(orders.FindAll(x => x.PostalId == id && x.DeliveryStatus == DeliveryStatus.Delivered));

        }

        public OrderDto ReserveOrder(int orderId, int postalId)
        {
            Order order = _deliveryContext.Orders.Find(orderId);
            order.PostalId = postalId;
            order.Postal = _deliveryContext.Users.Find(postalId);
            order.DeliveryStatus = DeliveryStatus.InProgress;
            _deliveryContext.SaveChanges();
            return _mapper.Map<OrderDto>(order);
        }
        public OrderDto OrderDelivered(int orderId)
        {
            Order order = _deliveryContext.Orders.Find(orderId);
            if(order == null)
                return null;
            order.DeliveryStatus = DeliveryStatus.Delivered;
            _deliveryContext.SaveChanges();
            return _mapper.Map<OrderDto>(order);
        }
    }
}
