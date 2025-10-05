using Microsoft.EntityFrameworkCore;
using MvcProgram.Models;
public class ProductService : IProductService
{
    private readonly IdentityContext _context;

    public ProductService(IdentityContext context)
    {
        _context = context;
    }

    public async Task<Product?> GetFormAsync(int? formId)
    {
        return await _context.Products.FindAsync(formId);
    }

    public async Task<bool> SubmitFormAsync(Product form)
    {
        _context.Products.Add(form);
        return await _context.SaveChangesAsync() > 0;
    }
    public async Task<List<Product>> GetGreaterThanAvgPrice(string userId)
    {   
        double? avg = _context.Products.Average(p=>p.price);
        return await _context.Products.Where(p => p.UID == userId && p.price >=avg ).ToListAsync();
    }
}
