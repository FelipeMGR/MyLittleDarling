using System.Reflection.Metadata.Ecma335;
using System;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[Authorize]
public class UsersController(DataContext context) : BaseApiController
{
    [Authorize]
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

    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
    {

        var users = await context.Users.ToListAsync();

        if (users == null)
        {
            return BadRequest("There's seen to be a error in your request. Please, try again.");
        }

        return Ok(users);
    }
}
