using MvcProgram.Models;
public interface IProductService
{
    Task<Product?> GetFormAsync(int? formId);
    Task<bool> SubmitFormAsync(Product product);
}
