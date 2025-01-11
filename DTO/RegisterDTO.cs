using System;

namespace API.DTO;

public class RegisterDTO
{
  public required string Username { get; set; }
  public required int Age { get; set; }
  public required string Password { get; set; }
}
