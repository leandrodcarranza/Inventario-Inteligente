using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SmartInventory.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SmartInventory.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {
        if (request.Username == "admin" &&
            request.Password == "1234")
        {
            return Ok(GenerateToken("admin"));
        }

        if (request.Username == "seller" &&
            request.Password == "1234")
        {
            return Ok(GenerateToken("seller"));
        }

        return Unauthorized();
    }

    private object GenerateToken(string role)
    {
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes("CLAVE_SUPER_SECRETA_PARA_JWT_2026_123456"));

        var creds = new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, role),
            new Claim(ClaimTypes.Role, role)
        };

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: creds
        );

        return new
        {
            token = new JwtSecurityTokenHandler()
                .WriteToken(token)
        };
    }
}