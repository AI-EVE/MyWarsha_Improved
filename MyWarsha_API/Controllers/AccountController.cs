using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyWarsha_Interfaces.ServicesInterfaces.IdentityServicesInterfaces;
using MyWarsha_Models.Models;

namespace MyWarsha_API.Controllers
{
[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly IJWTTokenGeneratorService _jwtTokenGeneratorService;

    public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IJWTTokenGeneratorService jwtTokenGeneratorService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtTokenGeneratorService = jwtTokenGeneratorService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterModel model)
    {
        var user = new AppUser { UserName = model.Username, Email = model.Email };
        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
            return Ok(new { Message = "User created successfully" });
        }

        return BadRequest(result.Errors);
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, false, false);

        if (result.Succeeded)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            var token = await _jwtTokenGeneratorService.GenerateJwtTokenAsync(user);
            return Ok(new { Token = token });
        }

        return Unauthorized();
    }

    [AllowAnonymous]
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return Ok(new { Message = "Logged out successfully" });
    }

    
}

}