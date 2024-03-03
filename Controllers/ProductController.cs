using BaseProject.API.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BaseProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly DataContext _context;
        public ProductController(DataContext dataContext)
        {
            _context = dataContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>>Get()
        {
            return Ok(await _context.Products.ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult<List<Product>>>Create(Product product)
        {
            _context.Add(product);
            await _context.SaveChangesAsync();

            return Ok( await _context.Products.ToListAsync());
        }
        
        [HttpPut]
        public async Task<ActionResult<List<Product>>>Update(Product product)
        {
            var prd = await _context.Products.FindAsync(product.Id);
            if (prd == null)
                return BadRequest("Product not found.");

            prd.Name = product.Name;
            prd.Code = product.Code;
            await _context.SaveChangesAsync();
            return Ok(await _context.Products.ToListAsync());

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Product>>> Delete(int id)
        {
            var prd = await _context.Products.FindAsync(id);
            if (prd == null)
                return BadRequest("Product not found.");

            _context.Products.Remove(prd);
            await _context.SaveChangesAsync();
            return Ok(await _context.Products.ToListAsync());
        }

    }
}
