using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Models;
using Models.Auth;

namespace TourApp.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private IConfiguration _config;
    private ApplicationDbContext _db;

    public AuthController(IConfiguration config, ApplicationDbContext applicationDbContext)
    {
        this._config = config;
        this._db = applicationDbContext;
    }
    [HttpPost("login")]
    public IActionResult Login(LoginUser _LoginUser)
    {
        var _user = Authenticate(_LoginUser);
        if (_user != null)
        {
            var token = GenerateToken(_user);
            return Ok(token);
        }
        return NotFound();
    }
    [HttpGet("logout")]
    public IActionResult Logout()
    {
        return Ok();
    }

    private string GenerateToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("JwtSettings:Key").Value));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.GivenName , user.DisplayName),
            new Claim(ClaimTypes.Email , user.Email),
            new Claim(ClaimTypes.Role , user.Role.ToString()),
        };
        var token = new JwtSecurityToken(
            _config.GetSection("JwtSettings:Issuer").Value,
            _config.GetSection("JwtSettings:Audience").Value,
            claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    private User Authenticate(LoginUser _LoginUser)
    {
        var _existingUser = _db.User.FirstOrDefault(u => u.Email == _LoginUser.Email);
        if (_existingUser != null && _existingUser.Password == _LoginUser.Password)
        {
            return _existingUser;
        }

        return null;
    }
    
    [HttpPost("signup")]
    public IActionResult Signup(SignupUser _SignupUser)
    {
        _db.User.Add(new User
        {
            Id = new Guid(),
            DisplayName = _SignupUser.DisplayName,
            Email = _SignupUser.Email,
            Password = _SignupUser.Password,
            Role = _SignupUser.Role
        });
        _db.SaveChanges();
        return Ok("User Created Successfully");
    }

    [HttpGet("test")]
    [Authorize(Roles = "Admin")]
    public IActionResult testRole(){
        return Ok("returned successfully");
    }
}
