
using MvcProgram.Models;
public interface IOrderService
{

    Task<Orders> submitOrderAsync(Orders order);
    Task<Orders?> getOrderAsync(Guid orderId);
    Task<List<Orders>> getOrdersByUserIdAsync(string userId);

    Task<bool> updateOrderStatusAsync(Guid orderId, Orders.OrderStatus status);

}