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


    [HttpPost("Register teacher")]
    public async Task<ActionResult<TempUser>>RegisterTeacher(UserDto request)
    {
        CreatePasswodHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
        user.UserName = request.UserName;
        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;

        var token = CreateTeacherToken(user);
        client = new User
        {
            Login = request.UserName,
            Password = request.Password,
            Role = "Teacher",
            Token = token
        };

        
        if (!(_db.SchoolUsers.Where(x => x.Login == request.UserName).Count() > 1))
        {
            _db.SchoolUsers.Add(client);
            await _db.SaveChangesAsync();
            return Ok(user);
        }
        else
        {
            return BadRequest("This login is already in use.");
        }
        
    }
    [HttpPost("Register student")]
    public async Task<ActionResult<TempUser>> RegisterStudent(UserDto request)
    {
        CreatePasswodHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
        user.UserName = request.UserName;
        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;

        var token = CreateStudentToken(user);
        client = new User
        {
            Login = request.UserName,
            Password = request.Password,
            Role = "Student",
            Token = token
        };

        if (!(_db.SchoolUsers.Where(x => x.Login == request.UserName).Count() > 1))
        {
            _db.SchoolUsers.Add(client);
            await _db.SaveChangesAsync();
            return Ok(user);
        }
        else
        {
            return BadRequest("This login is already in use.");
        }

    }


    [HttpPost("Login")]
    public ActionResult<string> Login(User request)
    {
        if (user.UserName != request.Login)
        {
            return BadRequest("User not found.");
        }

        if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
        {
            return BadRequest("Wrong password.");
        }

        if (!(_db.SchoolUsers.Select(x => x.Login == request.Login && x.Password == request.Password).Count() > 1))
        {
            return Ok(request.Token);
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