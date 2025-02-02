using System;
using API.Entities;
using API.Extensions;
using AutoMapper;

namespace API.DTO.Mapping;

public class UserDtoMapping : Profile
{
  public UserDtoMapping()
  {
    CreateMap<AppUser, MemberDTO>()
    .ForMember(d => d.Age, o => o.MapFrom(s => s.BirthDate.CalculateAge()))
    .ForMember(d => d.PhotoUrl, o => o.MapFrom(s => s.Photos.FirstOrDefault(x => x.IsMain)!.Url));

    CreateMap<Photo, PhotoDTO>();
  }
}
