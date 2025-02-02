using System.Reflection.Metadata.Ecma335;
using System;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Repositories;
using API.DTO;
using AutoMapper;

namespace API.Controllers;

[Authorize]
public class UsersController(IUserRepository userRepository) : BaseApiController
{
    [Authorize]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<MemberDTO>> GetUserById(int id)
    {
        MemberDTO? user = await userRepository.GetUserById(id);
        if (user == null)
        {
            return BadRequest("User not found");
        }

        return Ok(user);
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MemberDTO>>> GetUsers()
    {

        IEnumerable<MemberDTO?> users = await userRepository.GetAllMembersAsync();
        if (users == null)
        {
            return BadRequest("There's seen to be a error in your request. Please, try again.");
        }

        return Ok(users);
    }

    [HttpGet("{username}")]
    public async Task<ActionResult> GetUserByUserName(string name)
    {
        MemberDTO? user = await userRepository.GetMemberAsync(name);

        if (user == null) return NotFound("User not found");

        return Ok(user);
    }
}
