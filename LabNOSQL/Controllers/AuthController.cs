using LabNOSQL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;


namespace LabNOSQL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public static InternalUsers InternalUsersDto = new InternalUsers();
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<ActionResult<InternalUsers>> Register(AuthenticateUsers request)
        {
            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            InternalUsersDto.UserName = request.UserName;
            InternalUsersDto.PasswordHash = passwordHash;
            InternalUsersDto.PasswordSalt = passwordSalt;

            return Ok(InternalUsersDto);
        }
        
        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(AuthenticateUsers request)
        {
            if (InternalUsersDto.UserName != request.UserName)
            {
                return BadRequest("Usuario no encontrado");
            }
            if (!VerifyPasswordHash(request.Password, InternalUsersDto.PasswordHash, InternalUsersDto.PasswordSalt))
            {
                return BadRequest("Contraseña no coincide");
            }

            string token = CreateToken(request);
            return Ok(token);
        }
        
        private string CreateToken(AuthenticateUsers user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName)
            };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes((string)password));
                return computeHash.SequenceEqual(passwordHash);
            }
        }
    }
}
