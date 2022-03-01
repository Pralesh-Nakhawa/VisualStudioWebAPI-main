using Chip_Cart.Data;
using Chip_Cart.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chip_Cart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderDbContext _oContext;
        public OrderController(OrderDbContext orderDbContext)
        {
            _oContext = orderDbContext;
        }
        [HttpGet("get_all_orders")]
        public IActionResult GetAllOrders()
        {
            var orders = _oContext.orderModels.AsQueryable();
            return Ok(new
            {
                StatusCode = 200,
                UserDetails = orders
            });
        }
        [HttpPost("place_order")]
        public IActionResult AddProduct([FromBody] OrderModel orderobj)
        {
            if (orderobj == null)
            {
                return BadRequest();
            }
            else
            {                         
                orderobj.orderdate = DateTime.Now;
                _oContext.orderModels.Add(orderobj);
                _oContext.SaveChanges();
                return Ok(new
                {
                    StatusCode = 200,
                    Message = "Orderd Sucessfull"
                });
            }
        }
        [HttpGet("get_orders_by_id/{userid}")]
        public IActionResult GetOrdersbyId(int userid)
        {
            var orders = _oContext.orderModels.Where(u => u.userid == userid);
            if (orders == null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "userid Not Found"
                });
            }
            return Ok(new
            {
                StatusCode = 200,
                orderDetails = orders
            });
        }
        [HttpDelete("cancel_order/{id}")]
        public IActionResult CancelOrder(int id)
        {
            var order = _oContext.orderModels.Find(id);
            if (order == null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "id Not Found"
                });
            }
            _oContext.Remove(order);
            _oContext.SaveChanges();
            return Ok(new
            {
                StatusCode = 200,
                Message = "Order Canceled"
            });
        }
    }
}
