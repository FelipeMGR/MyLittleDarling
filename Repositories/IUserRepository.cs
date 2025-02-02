using System;
using API.DTO;
using API.Entities;

namespace API.Repositories;

public interface IUserRepository
{
  void Update(AppUser user);
  Task<bool> SaveAllAsync();
  Task<MemberDTO?> GetUserById(int id);
  Task<IEnumerable<MemberDTO>> GetAllMembersAsync();
  Task<MemberDTO?> GetMemberAsync(string username);
}
