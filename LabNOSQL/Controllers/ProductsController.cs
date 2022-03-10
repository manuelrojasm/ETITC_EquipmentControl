using LabNOSQL.Models;
using LabNOSQL.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LabNOSQL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IProductsCollection db = new ProductsCollection();
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            return Ok(await db.GetAllProducts());
        }
        [HttpGet("{​id}​")]
        public async Task<IActionResult> GetReadProduct(string id)
        {
            return Ok(await db.ReadProduct(id));
        }
        [HttpPost()]
        public async Task<IActionResult> CreateProduct([FromBody] Products product)
        {
            if (product == null)
                return BadRequest();
            if (product.Name == string.Empty)
                ModelState.AddModelError("Name", "el producto no puede ser vacio");
            await db.CreateProduct(product);
            return Created("Created", true);
        }
        [HttpPut("{​id}​")]
        public async Task<IActionResult> UpdateProduct([FromBody] Products product, string id)
        {
            if (product == null)
                return BadRequest();
            if (product.Name == string.Empty)
                ModelState.AddModelError("Name", "el producto no puede ser vacio");
            product.Id = new MongoDB.Bson.ObjectId(id);
            await db.UpdateProduct(product);
            return Created("Actulizado", true);
        }

        [HttpDelete("{​id}​")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            await db.DeleteProduct(id);
            return NoContent();
        }

    }
}
