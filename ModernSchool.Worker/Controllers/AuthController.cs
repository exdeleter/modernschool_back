using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ModernSchool.Worker.Authorization;
using ModernSchool.Worker.Contexts;
using ModernSchool.Worker.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ModernSchool.Worker.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    
    public static Authorization.User user = new Authorization.User();
    private readonly IConfiguration _configuration;
    private SchoolDBContext db;

    public AuthController(IConfiguration configuration, SchoolDBContext db)
    {
        _configuration = configuration;
        this.db = db;
    }

    private void CreatePasswodHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512())
        {
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }
    }

    private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512(passwordSalt))
        {
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(passwordHash); 
        } 
    }

    [HttpPost("register")]
    public async Task<ActionResult<Authorization.User>>Register(UserDto request)
    {
        CreatePasswodHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
        user.UserName = request.UserName;
        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;
        
        return Ok(user);
    }

    [HttpPost("LoginAsTeacher")]
    public async Task<ActionResult<string>> LoginAsTeacher(UserDto request)
    {
        if (user.UserName != request.UserName) 
        {
            return BadRequest("User not found.");
        }

        if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
        {
            return BadRequest("Wrong password.");
        }

        string token = CreateTeacherToken(user);

        Models.User client = new Models.User();
        client.Login = request.UserName;
        client.Password = request.Password;
        client.Role = "Teacher";
        db.Users.Add(client);
        await db.SaveChangesAsync();

        return Ok(token);
    }

    [HttpPost("LoginAsStudent")]
    public async Task<ActionResult<string>> LoginAsStudent(UserDto request)
    {
        if (user.UserName != request.UserName)
        {
            return BadRequest("User not found.");
        }

        if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
        {
            return BadRequest("Wrong password.");
        }

        string token = CreateStudentToken(user);

        Models.User client = new Models.User();
        client.Login = request.UserName;
        client.Password = request.Password;
        client.Role = "Student";
        db.Users.Add(client);
        await db.SaveChangesAsync();

        return Ok(token);
    }


    private string CreateTeacherToken(Authorization.User user)
    {
        List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Role, "Teacher")
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            _configuration.GetSection("AppSettings:Token").Value));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: creds
        );
        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }

    private string CreateStudentToken(Authorization.User user)
    {
        List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Role, "Student")
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            _configuration.GetSection("AppSettings:Token").Value));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: creds
        );
        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }
}