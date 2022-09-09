using Microsoft.AspNetCore.Mvc;
using RealEstateAPI.Data;
using RealEstateAPI.Models;

namespace RealEstateAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private DBContextRealEstate _context = new DBContextRealEstate();
        private IConfiguration _configuration;

        public UsersController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("[action]")]
        public IActionResult Register([FromBody] User model)
        {
            try
            {
                var userExists = _context.Users.FirstOrDefault(u => u.Email.ToLower().Trim() == model.Email.ToLower().Trim());
                if (userExists != null)
                {
                    return NotFound(String.Format("User with {0} email already exists.", model.Email));
                }
                else
                {
                    _context.Users.Add(model);
                    _context.SaveChanges();
                    return StatusCode(StatusCodes.Status201Created);
                }
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
                var userExists = _context.Users.FirstOrDefault(u => u.Email.ToLower().Trim() == model.Email.ToLower().Trim() &&
                                                               u.Password.ToLower().Trim() == model.Password.ToLower().Trim());
                if (userExists != null)
                {
                    return NotFound(String.Format("User with {0} email already exists.", model.Email));
                }
                else
                {
                    // Generate JWT
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}