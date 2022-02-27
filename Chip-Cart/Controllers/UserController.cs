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
    public class UserController : ControllerBase
    {
        private readonly UserDbContext _uContext;
        public UserController(UserDbContext userDbContext)
        {
            _uContext = userDbContext;
        }
        [HttpGet("get_all_users")]
        public IActionResult GetAllUsers()
        {
            var users = _uContext.userModels.AsQueryable();
           
          
            return Ok(new
            {
                StatusCode = 200,
                UserDetails = users
            });
        }
        [HttpGet("get_user_by_id/{id}")]
        public IActionResult GetUser(int id)
        {
            var user = _uContext.userModels.Find(id);
             
            if (user == null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "User Not Found"
                });
            }
            return Ok(new
            {
                StatusCode = 200,
                UserDetails = user
            });
        }
        [HttpPost("signUp")]
        public IActionResult Signup([FromBody] UserModel userobj)
        {
            if (userobj == null)
            {
                return BadRequest();
            }
            else
            {
                userobj.createdAt = DateTime.Now;
                userobj.updatedAt = DateTime.Now;
                _uContext.userModels.Add(userobj);
                _uContext.SaveChanges();
                return Ok(new
                {
                    StatusCode = 200,
                    Message = "Signup Sucessfull"
                });

            }


        }
        [HttpPut("updateUser")]
        public IActionResult UpdateUser([FromBody] UserModel userobj)
        {
            if (userobj == null)
            {
                return BadRequest();
            }

            var product = _uContext.userModels.AsNoTracking().FirstOrDefault(x => x.id == userobj.id);
            if (product == null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "User Not Found"
                });
            }
            else
            {
                userobj.createdAt = DateTime.Now;
                userobj.updatedAt = DateTime.Now;
                _uContext.Entry(userobj).State = EntityState.Modified;
                _uContext.SaveChanges();
                return Ok(new
                {
                    StatusCode = 200,
                    Message = "User Updated"
                });

            }
        }

        [HttpDelete("deleteUser/{id}")]
        public IActionResult DeleteUser(int id)
        {

            var user = _uContext.userModels.Find(id);
            if (user == null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "User Not Found"
                });
            }
            else
            {
                user.createdAt = DateTime.Now;
                user.updatedAt = DateTime.Now;
                _uContext.Remove(user);
                _uContext.SaveChanges();
                return Ok(new
                {
                    StatusCode = 200,
                    Message = "User deleted"
                });

            }
        }
    }
}
