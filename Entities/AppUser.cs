using System;

namespace API.Entities;

public class AppUser
{

  public int Id { get; set; }
  public required string Name { get; set; }
  public int Age { get; set; }

  public AppUser(int id, string name, int age)
  {
    Id = id;
    Name = name;
    Age = age;
  }
}
