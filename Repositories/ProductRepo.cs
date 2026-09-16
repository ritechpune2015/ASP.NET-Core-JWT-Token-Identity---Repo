using CURDUSingAPIEFCore.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;

namespace CURDUSingAPIEFCore.Repositories
{
    public class ProductRepo : IProduct
    {
        CompanyContext cc;
        public ProductRepo(CompanyContext _cc)
        {
            cc = _cc;
        }   
        public async Task<Product> AddProduct(Product product)
        {
           await this.cc.Products.AddAsync(product);
            await this.cc.SaveChangesAsync();

            return product;
        }

        public async Task DeleteProduct(long id)
        {
            var rec=await this.cc.Products.FindAsync(id);
            this.cc.Products.Remove(rec);   
            await this.cc.SaveChangesAsync();
        }

        public async Task<Product> GetProductById(long id)
        {
            return await this.cc.Products.FindAsync(id);
        }

        public async Task<List<Product>> GetProducts()
        {
            return await this.cc.Products.ToListAsync();
        }

        public async Task PatchProduct(JsonPatchDocument<Product> rec, Product oldrec)
        {
            rec.ApplyTo(oldrec);
            await this.cc.SaveChangesAsync();
        }

        public async Task UpdateProduct(Product product)
        {
          this.cc.Products.Update(product);
          await this.cc.SaveChangesAsync();
        }
    }
}
