using System;
using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTO;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class AccountController(DataContext context, ITokenService tokenService) : BaseApiController
{
  [HttpPost("register")]
  public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDTO)
  {
    if (await UserExists(registerDTO.Username)) return BadRequest("Username already exists");
    using var hmac = new HMACSHA512();

    var user = new AppUser
    {
      Name = registerDTO.Username.ToLower(),
      Age = registerDTO.Age,
      PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDTO.Password)),
      PasswordSalt = hmac.Key
    };

    context.Users.Add(user);
    await context.SaveChangesAsync();

    return Ok(new UserDTO()
    {
      UserName = user.Name,
      Token = tokenService.CreateToken(user)
    });
  }

  [HttpPost("login")]
  public async Task<ActionResult<UserDTO>> Login(LoginDTO login)
  {
    var user = await context.Users.FirstOrDefaultAsync(x => x.Name == login.UserName.ToLower());

    if (user is null) return Unauthorized("Login is invalid.");

    using var hmac = new HMACSHA512(user.PasswordSalt);

    var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(login.Password));

    for (int i = 0; i < computedHash.Length; i++)
    {
      if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Login is invalid");
    }

    return Ok(new UserDTO()
    {
      UserName = user.Name,
      Token = tokenService.CreateToken(user)
    });
  }
  public async Task<bool> UserExists(string userName)
  {
    return await context.Users.AnyAsync(x => x.Name.ToLower() == userName.ToLower());
  }

}
