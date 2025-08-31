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
}
