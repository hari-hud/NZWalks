using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalks.Data;
using NZWalks.Models.DTO;
using NZWalks.Repositories;

namespace NZWalks.Controllers;

[ApiController]
[Route("api/[controller]")]
// [Authorize]
public class AuthController : ControllerBase
{
    private readonly UserManager<IdentityUser> userManager;
    private readonly ITokenRepository tokenRepository;

    public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
    {
        this.userManager = userManager;
        this.tokenRepository = tokenRepository;
    }

    [HttpPost]
    [Route("Register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
    {
        var identityUser = new IdentityUser
        {
            UserName = registerRequestDto.Username,
            Email = registerRequestDto.Username
        };

        var identityResult = await userManager.CreateAsync(identityUser, registerRequestDto.Password);
    
        if (identityResult.Succeeded)
        {
            // add roles to this user
            if (registerRequestDto.Roles != null &&registerRequestDto.Roles.Any())
            {
                identityResult = await userManager.AddToRolesAsync(identityUser, registerRequestDto.Roles);
            }

            if (identityResult.Succeeded) 
            {
                return Ok("User registered! Please login.");
            }
        }

        return BadRequest("Something went wrong");
    }

    [HttpPost]
    [Route("Login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
    {
        var user = await userManager.FindByEmailAsync(request.Username);

        if (user != null) {
            var isPasswordValid = await userManager.CheckPasswordAsync(user, request.Password);

            if (isPasswordValid) {
                // create token and return in response
                var roles = await userManager.GetRolesAsync(user);

                var jwtToken = tokenRepository.CreateJWTToken(user, roles.ToList());

                // In future this class can be used to return add'l user details in response
                // like email, roles, etc.
                var response = new LoginResponseDto {
                    JwtToken = jwtToken
                };

                return Ok(response);
            }
        }

        return BadRequest("Username or password incorrect");
    }
}
