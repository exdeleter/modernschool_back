using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ModernSchool.Worker.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using ModernSchool.Worker.Contexts;
using ModernSchool.Worker.Models;

namespace ModernSchool.Worker.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    // TODO: Добавить юзеров в бд
    public static TempUser user = new();
    private readonly IConfiguration _configuration;
    private SchoolDBContext _db;

    private User client;
    public AuthController(IConfiguration configuration, SchoolDBContext db)
    {
        _configuration = configuration;
        _db = db;
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
    public async Task<ActionResult<TempUser>>Register(UserDto request)
    {
        CreatePasswodHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
        user.UserName = request.UserName;
        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;

        client = new User
        {
            Login = request.UserName,
            Password = request.Password,
            Role = "None"
        };

        if (!(_db.SchoolUsers.Contains(client))) 
        {
            _db.SchoolUsers.Add(client);
            await _db.SaveChangesAsync();
            return Ok(user);
        }
        else
        {
            return BadRequest();
        }
        
    }

    [HttpPost("LoginAsTeacher")]
    public async Task<ActionResult<string>> LoginAsAdmin(UserDto request)
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

        if (!(_db.SchoolUsers.Select(x => x.Login == request.UserName && x.Password == request.Password).Count() > 1))
        {
            client.Role = "Teacher";
            await _db.SaveChangesAsync();
            return Ok(token);
        }
        else
        {
            return BadRequest();
        }
        
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

        if (!(_db.SchoolUsers.Select(x => x.Login == request.UserName && x.Password == request.Password).Count() > 1))
        {
            client.Role = "Student";
            await _db.SaveChangesAsync();
            return Ok(token);
        }
        else
        {
            return BadRequest();
        }
        
    }


    private string CreateTeacherToken(TempUser user)
    {
        List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Role, "Admin")
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

    private string CreateStudentToken(TempUser user)
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