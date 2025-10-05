
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MvcProgram.Models;
public class OrderService : IOrderService
{
    IdentityContext _context;

    public OrderService(IdentityContext context)
    {
        _context = context;
    }
    public async Task<Orders> submitOrderAsync(Orders order)
    {

        order.OrderID = Guid.NewGuid();
        order.Status = Orders.OrderStatus.Pending;
        _context.Orders.AddAsync(order);
        return await _context.SaveChangesAsync().ContinueWith(t => order); 

    }
    public Task<Orders?> getOrderAsync(Guid orderId)
    {
        return Task.FromResult<Orders?>(null);
    }
    public Task<List<Orders>> getOrdersByUserIdAsync(string userId)
    {
        return Task.FromResult(new List<Orders>());
    }

    
    public Task<bool> updateOrderStatusAsync(Guid orderId, Orders.OrderStatus status)
    {
        return Task.FromResult(true);
    }

}