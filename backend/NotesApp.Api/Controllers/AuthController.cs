using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NotesApp.Data;
using NotesApp.Data.Entities;

namespace NotesApp.Api.Controllers;

public record RegisterDto(
    [Required][EmailAddress] string Email,
    [Required] string Password,
    [Required] string Name
);

public record LoginDto(
    [Required][EmailAddress] string Email,
    [Required] string Password
);

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly IConfiguration _config;
    private readonly AppDbContext _db;

    public AuthController(
        UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
        IConfiguration config, AppDbContext db) {
        _userManager  = userManager;
        _signInManager = signInManager;
        _config = config;
        _db = db;
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto dto)
    {
        var user = new AppUser { UserName = dto.Email, Email = dto.Email, Name = dto.Name };
        var result = await _userManager.CreateAsync(user, dto.Password);

        if (!result.Succeeded) return BadRequest(result.Errors);

        // seed Default & Trash folders
        _db.Folders.AddRange(
            new Folder { Name = "Notes", Type = FolderType.Default, UserId = user.Id },
            new Folder { Name = "Trash", Type = FolderType.Trash, UserId = user.Id }
        );
        await _db.SaveChangesAsync();

        return Created();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto) {
        var user = await _userManager.FindByEmailAsync(dto.Email);
        if (user == null) return Unauthorized("Invalid credentials");

        var signInResult = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
        if (!signInResult.Succeeded) return Unauthorized("Invalid credentials");
        
        var token  = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            claims:
            [
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email)
            ],
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"])), SecurityAlgorithms.HmacSha256)
        );
        
        return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token), expiration = token.ValidTo });
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout() {
        await _signInManager.SignOutAsync();
        return Ok();
    }
}