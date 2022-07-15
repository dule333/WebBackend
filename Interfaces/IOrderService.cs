using System.Collections.Generic;
using WebBackend.Dto;

namespace WebBackend.Interfaces
{
    public interface IOrderService
    {
        List<OrderDto> GetOrders(int id);
        List<OrderDto> GetOrdersAdmin();
        List<OrderDto> GetOrdersPostal();
        List<OrderDto> GetOrdersPostalHistory(int id);

        OrderDto CreateOrder(OrderDto order, int id);
        OrderDto ReserveOrder(int orderId, int postalId);
        OrderDto GetOrder(int orderId);
        OrderDto OrderDelivered(int orderId);
    }
}
