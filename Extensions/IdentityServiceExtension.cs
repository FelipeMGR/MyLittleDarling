using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace API.Extensions;

public static class IdentityServiceExtension
{
  public static IServiceCollection AddAuthenticationService(this IServiceCollection services, IConfiguration configuration)
  {
    services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
      .AddJwtBearer(options =>
      {
        var tokenKey = configuration["TokenKey"] ?? throw new Exception("Token not found");
        options.TokenValidationParameters = new TokenValidationParameters
        {
          ValidateIssuerSigningKey = true, //the key must be a signed one, not any key.
          IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey)),
          ValidateIssuer = false,
          ValidateAudience = false
        };
      });
    return services;
  }
}
