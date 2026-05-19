using System.Security.Claims;
using ApiProjectPractise.Dtos.UserDtos;
using ApiProjectPractise.Models;
using ApiProjectPractise.Services;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ApiProjectPractise.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(
        IValidator<RegisterDto> validator,
        UserManager<AppUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IMapper mapper,
        IConfiguration config,
        JwtService jwtService
        ) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var validationResult=validator.Validate(registerDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            var user=await userManager.FindByNameAsync(registerDto.UserName);
            if (user is not null)
            {
                return BadRequest("UserName already exists");
            }
          user=mapper.Map<AppUser>(registerDto);
            var result=await userManager.CreateAsync(user,registerDto.Password);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            await userManager.AddToRoleAsync(user, "Member");
            return Ok("user registered successfully");         
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var user = await userManager.FindByNameAsync(loginDto.UserName);
            if (user is null)
            {
                return BadRequest("Invalid username or password");
            }
            var isPasswordValid = await userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!isPasswordValid)
            {
                return BadRequest("Invalid username or password");
            }
            var roles = await userManager.GetRolesAsync(user);

            return Ok(
                new
                {
                    token= jwtService.GenerateToken(user, roles,config)
                });
        }

        [HttpGet]
        [Authorize]
        public IActionResult UserProfile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userName = User.Identity?.Name;
            var fullName = User.FindFirstValue("Fullname");
            var roles = User.FindAll(ClaimTypes.Role).Select(r => r.Value).ToList();
            return Ok(new
            {
                userId,
                userName,
                fullName,
                roles
            });
        }

        //[HttpGet]
        //public async Task<IActionResult> CreateRole()
        //{
        //    await roleManager.CreateAsync(new IdentityRole("Member"));
        //    await roleManager.CreateAsync(new IdentityRole("Admin"));

        //    return Ok();
        //}
    }
}
