using AutoMapper;
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
            Order newOrder = new Order
            {
                Id = order.Id,
                Comment = order.Comment,
                TotalPrice = 150,
                CustomerId = id,
                Customer = _deliveryContext.Users.Find(id),
                DeliveryStatus = DeliveryStatus.Free,
                EndTime = order.EndTime,
                OrderProducts = new List<OrderProduct>()
            };

            foreach (var item in order.products)
            {
                OrderProduct orderProduct = new OrderProduct
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
            return _mapper.Map<OrderDto>(newOrder);
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

        public List<OrderDto> GetOrdersAdmin(int id)
        {
            return _mapper.Map<List<OrderDto>>(_deliveryContext.Orders);
        }

        public List<OrderDto> GetOrdersPostal(int id)
        {
            List<Order> orders = _mapper.Map<List<Order>>(_deliveryContext.Orders);
            return _mapper.Map<List<OrderDto>>(orders.FindAll(x => x.PostalId == id && x.DeliveryStatus == DeliveryStatus.Free));

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
    }
}
