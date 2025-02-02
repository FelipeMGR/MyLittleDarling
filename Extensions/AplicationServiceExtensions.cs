using System.Text.Json;
using API.Data;
using API.DTO.Mapping;
using API.Interfaces;
using API.Repositories;
using API.Services;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions;

public static class AplicationServiceExtensions
{
  public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
  {
    services.AddControllers();

    services.AddDbContext<DataContext>(opt =>
    {
      opt.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
    });

    services.AddCors();
    services.AddScoped<ITokenService, TokenService>();
    services.AddScoped<IUserRepository, UserRepository>();
    services.AddAutoMapper(typeof(UserDtoMapping));

    return services;
  }
}
