using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RealEstateAPI.Data;
using RealEstateAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RealEstateAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private DBContextRealEstate _context;
        private IConfiguration _configuration;

        public UsersController(DBContextRealEstate context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("[action]")]
        public IActionResult Register([FromBody] User model)
        {
            try
            {
                //  Check if User exists
                var userExists = _context.Users.FirstOrDefault(u => u.Email.ToLower().Trim() == model.Email.ToLower().Trim());
                if (userExists != null) return NotFound(String.Format("User with {0} email already exists.", model.Email));
                //  Save Record
                _context.Users.Add(model);
                _context.SaveChanges();
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("[action]")]
        public IActionResult Login([FromBody] User model)
        {
            try
            {
                //  Check if User exists
                var userExists = _context.Users.FirstOrDefault(u => u.Email.ToLower().Trim() == model.Email.ToLower().Trim() &&
                                                               u.Password.ToLower().Trim() == model.Password.ToLower().Trim());
                if (userExists == null) return NotFound(String.Format("User with {0} email not found.", model.Email));

                // Generate JWT
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                //  Add Claims
                var tokenClaims = new[]
                {
                        new Claim(type: "UserID", value:model.UserID.ToString()),
                        new Claim(ClaimTypes.Email, model.Email)
                    };
                //  Create Token
                var jwtTokeConfig = new JwtSecurityToken(issuer: _configuration["JWT:KeyIssuer"], audience: _configuration["JWT:Audience"], claims: tokenClaims, expires: DateTime.Now.AddMinutes(60), signingCredentials: credentials);
                //  Token String
                var jwtToken = new JwtSecurityTokenHandler().WriteToken(jwtTokeConfig);
                return Ok(jwtToken);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}