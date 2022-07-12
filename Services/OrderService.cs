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
            Order newOrder = _mapper.Map<Order>(order);
            newOrder.CustomerId = id;
            newOrder.Customer = _deliveryContext.Users.Find(id);
            _deliveryContext.Orders.Add(newOrder);
            _deliveryContext.SaveChanges();
            return _mapper.Map<OrderDto>(newOrder);
        }

        public OrderDto GetOrder(int orderId)
        {
            throw new System.NotImplementedException();
        }

        public List<OrderDto> GetOrders(int id)
        {
            throw new System.NotImplementedException();
        }

        public List<OrderDto> GetOrdersAdmin(int id)
        {
            throw new System.NotImplementedException();
        }

        public List<OrderDto> GetOrdersPostal(int id)
        {
            throw new System.NotImplementedException();
        }

        public List<OrderDto> GetOrdersPostalHistory(int id)
        {
            throw new System.NotImplementedException();
        }

        public OrderDto ReserveOrder(int orderId, int postalId)
        {
            throw new System.NotImplementedException();
        }
    }
}
