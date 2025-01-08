using System.Reflection.Metadata.Ecma335;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController(DataContext context) : ControllerBase
    {
        [HttpGet("{id:int}")]
        public async Task<ActionResult<AppUser>> GetUser(int id)
        {
            var user = await context.Users.FindAsync(id);

            if (user == null)
            {
                return BadRequest("User not found");
            }

            return Ok(user);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> ListedUsers()
        {

            var users = await context.Users.Take(10).AsNoTracking().ToListAsync();

            if (users == null)
            {
                return BadRequest("There's seen to be a error in your request. Please, try again.");
            }

            return Ok(users);
        }
    }
}
