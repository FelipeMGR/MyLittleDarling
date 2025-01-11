using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using API.Entities;
using API.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace API.Services;

public class TokenService(IConfiguration configuration) : ITokenService
{
  public string CreateToken(AppUser user)
  {
    var tokenKey = configuration["TokenKey"] ?? throw new Exception("Could not acess the token on appsettings"); //verifies the appsettings for a token
    if (tokenKey.Length < 64) throw new Exception("Tokenkey must be longer"); //verifies if the token has more than 64 characters

    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey)); //creates a signature key

    var claims = new List<Claim>(){ //especifies the claim for the user
      new(ClaimTypes.NameIdentifier, user.Name)
    };

    var tokenDescriptor = new SecurityTokenDescriptor()
    {
      Subject = new ClaimsIdentity(claims), //especifies the claims for the token
      Expires = DateTime.Now.AddDays(7), //especifies expiration time for the token
      SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature) //uses the key to create a credential signature for the token
    };

    var tokenHandler = new JwtSecurityTokenHandler();
    var token = tokenHandler.CreateToken(tokenDescriptor); //creates the token

    return tokenHandler.WriteToken(token); //return and writes the token on the screen
  }
}
