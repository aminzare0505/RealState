using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RealState.Data;
using RealState.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RealState.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private IConfiguration _configuration;
        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpPost("[action]")]
       public IActionResult Register([FromBody] User user)
        {
         var userExist=DbContext.Users.FirstOrDefault(f=>f.Email==user.Email);
            if (userExist != null)
            {
                return BadRequest("User is exists");
            }
            DbContext.Users.Add(user);
            DbContext.SaveChanges();
            return StatusCode(StatusCodes.Status201Created);
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(DbContext.Users);
        }

        [HttpPost("[action]")]
        public IActionResult Login([FromBody] User user)
        {
            var currentUser = DbContext.Users.FirstOrDefault(f => f.Email == user.Email && f.Password==user.Password);
            if (currentUser == null)
            {
                return NotFound("User is not exists");
            }
            var SecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
           var Credential= new SigningCredentials(SecurityKey,SecurityAlgorithms.HmacSha256);
            var Claims = new[]
            {
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.Name,user.Name),
            };
            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                claims:Claims,
                expires:DateTime.Now.AddMinutes(60),
                signingCredentials:Credential
                );
            var JWT=new JwtSecurityTokenHandler().WriteToken(token);
            return Ok(JWT);
        }
    }
}
