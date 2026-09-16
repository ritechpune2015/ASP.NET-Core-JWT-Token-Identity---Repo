using CURDUSingAPIEFCore.Models;
using CURDUSingAPIEFCore.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CURDUSingAPIEFCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController] // validation automatically 
    public class ProductAPIController : ControllerBase
    {

        IProduct repo;
        public ProductAPIController(IProduct _repo)
        {
            repo = _repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var res = await this.repo.GetProducts();
            if (res == null)
                return BadRequest();
            return Ok(res);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProducts(Int64 id)
        {
            var res = await this.repo.GetProductById(id);
            if (res == null)
                return NotFound("Product Not Found!");
            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product rec)
        { 
            var res = await this.repo.AddProduct(rec);
            if (res == null)
                return BadRequest();
            // return Created();
            //  return Created("/api/ProductAPI", res);
            return Ok("Product Created!");
        }

        [HttpPut()]
        public async Task<IActionResult> Update(Product rec)
        {
            if (rec == null)
                return BadRequest();

            await this.repo.UpdateProduct(rec);
            //return NoContent();
            return Ok("Product Updated!");
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Int64 id)
        { 
          if(id==0) return BadRequest();
          await this.repo.DeleteProduct(id);
            // return NoContent();
            return Ok("Product Deleted!");
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateCustomer(Int64 id,JsonPatchDocument<Product> rec)
        {
            var oldrec = await this.repo.GetProductById(id);
            if (oldrec == null)
                return BadRequest();

            await this.repo.PatchProduct(rec, oldrec);
            return Ok("Product updated!");
        }

    }
}
