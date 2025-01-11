using System;
using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTO;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class AccountController(DataContext context) : BaseApiController
{
  [HttpPost("register")]
  public async Task<ActionResult<AppUser>> Register(RegisterDTO registerDTO)
  {
    if (await UserExists(registerDTO.Username)) return BadRequest("Username already exists");
    using var hmac = new HMACSHA512();

    var user = new AppUser
    {
      Name = registerDTO.Username.ToLower(),
      PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDTO.Password)),
      PasswordSalt = hmac.Key
    };

    context.Users.Add(user);
    await context.SaveChangesAsync();

    return Ok(user);
  }

  public async Task<bool> UserExists(string userName)
  {
    return await context.Users.AnyAsync(x => x.Name.ToLower() == userName.ToLower());
  }

}
