using CURDUSingAPIEFCore.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace CURDUSingAPIEFCore.Repositories
{
    public interface IProduct
    {
        Task<List<Product>> GetProducts();  
        Task<Product> GetProductById(Int64 id);
        Task<Product> AddProduct(Product product);
        Task UpdateProduct(Product product);

        Task PatchProduct(JsonPatchDocument<Product> doc,Product product);
        Task DeleteProduct(Int64 id);
    }
}
