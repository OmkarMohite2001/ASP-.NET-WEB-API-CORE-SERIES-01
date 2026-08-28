using EVChargingManagementAPI.Data;
using EVChargingManagementAPI.DTOs;
using EVChargingManagementAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EVChargingManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        public AuthController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u=>u.Email == registerDto.Email);
            if (existingUser != null)
            {
                return BadRequest(new

                {
                    messege = "Email Already Registered."
                });
            }
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);

            var user = new User
            {
                FullName = registerDto.FullName,
                Email = registerDto.Email,
                PasswordHash = passwordHash,
                Role = "Customer"
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok(
                new
                {
                    messege = "Registration Successful.",
                    userId = user.Id,
                    fullName = user.FullName,
                    email = user.Email,
                    role = user.Role
                });
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u=>u.Email ==loginDto.Email);
            if (user == null)
            {
                return Unauthorized(new
                {
                    messege = "Invalid Email or Password."
                });

            }
            bool passwordValid = BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash);
            if (!passwordValid)
            {
                return Unauthorized(new
                {
                    messege = "Invalid Password..."
                });
            }
            var claims = new List<Claim>
                {
                    new Claim(
                        ClaimTypes.NameIdentifier,
                        user.Id.ToString()
                    ),

                    new Claim(
                        ClaimTypes.Name,
                        user.Email
                    ),

                    new Claim(
                        ClaimTypes.Role,
                        user.Role
                    )
                 };
            var key = new SymmetricSecurityKey(
        Encoding.UTF8.GetBytes(
            _configuration["Jwt:Key"]!
        )
    );

            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256
            );

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],

                audience: _configuration["Jwt:Audience"],

                claims: claims,

                expires: DateTime.UtcNow.AddMinutes(
                    Convert.ToDouble(
                        _configuration["Jwt:ExpireMinutes"]
                    )
                ),

                signingCredentials: credentials
            );

            var tokenString = new JwtSecurityTokenHandler()
                .WriteToken(token);

            return Ok(new
            {
                message = "Login successful",

                token = tokenString,

                expires = token.ValidTo,

                user = new
                {
                    user.Id,
                    user.Email,
                    user.Role
                }
            });
        }
    }
}
