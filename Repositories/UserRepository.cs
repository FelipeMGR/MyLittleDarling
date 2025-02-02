using System;
using API.Data;
using API.DTO;
using API.Entities;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories;

public class UserRepository(DataContext context, IMapper mapper) : IUserRepository
{
  public async Task<IEnumerable<MemberDTO>> GetAllMembersAsync()
  {
    return await context.Users
    .ProjectTo<MemberDTO>(mapper.ConfigurationProvider)
    .ToListAsync();
  }

  public async Task<MemberDTO?> GetMemberAsync(string name)
  {
    return await context.Users
    .Where(u => u.UserName == name)
    .ProjectTo<MemberDTO>(mapper.ConfigurationProvider)
    .SingleOrDefaultAsync();
  }

  public async Task<MemberDTO?> GetUserById(int id)
  {
    return await context.Users
    .Where(x => x.Id == id)
    .ProjectTo<MemberDTO>(mapper.ConfigurationProvider)
    .FirstOrDefaultAsync();
  }

  public async Task<bool> SaveAllAsync()
  {
    return await context.SaveChangesAsync() > 0;
  }

  public void Update(AppUser user)
  {
    context.Entry(user).State = EntityState.Modified;
  }
}
