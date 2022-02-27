using Chip_Cart.Data;
using Chip_Cart.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chip_Cart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductDbContext _context;
        public ProductsController(ProductDbContext productDbContext)
        {
            _context = productDbContext;
        }
        [HttpGet("get_all_products")]
        public IActionResult GetAllProducts()
        {
            var products = _context.ProductModels.AsQueryable();
            return Ok(new
            {
                StatusCode = 200,
                ProductDetails = products
            });
        }

        [HttpPost("addProduct")]
        public IActionResult AddProduct([FromBody] ProductsModel productobj)
        {
            if (productobj == null)
            {
                return BadRequest();
            }
            else
            {
                productobj.createdAt = DateTime.Now;
                productobj.updatedAt = DateTime.Now;
                _context.ProductModels.Add(productobj);
                _context.SaveChanges();
                return Ok(new
                {
                    StatusCode = 200,
                    Message = "Product Added"
                });

            }


        }

        [HttpPut("updateProduct")]
        public IActionResult UpdateProduct([FromBody] ProductsModel productobj)
        {
            if (productobj == null)
            {
                return BadRequest();
            }
            var product = _context.ProductModels.AsNoTracking().FirstOrDefault(x => x.id == productobj.id);
            if (product == null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "Product Not Found"
                });
            }
            else
            {
                productobj.createdAt = DateTime.Now;
                productobj.updatedAt = DateTime.Now;
                _context.Entry(productobj).State = EntityState.Modified;
                _context.SaveChanges();
                return Ok(new
                {
                    StatusCode = 200,
                    Message = "Product Added"
                });

            }
        }

        [HttpDelete("deleteProduct/{id}")]
        public IActionResult DeleteProduct(int id)
        {

            var product = _context.ProductModels.Find(id);
            if (product == null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "Product Not Found"
                });
            }
            else
            {
                product.createdAt = DateTime.Now;
                product.updatedAt = DateTime.Now;
                _context.Remove(product);
                _context.SaveChanges();
                return Ok(new
                {
                    StatusCode = 200,
                    Message = "Product deleted"
                });

            }
        }

    }
}
