using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstateAPI_Auth0.Data;
using RealEstateAPI_Auth0.Models;
using System.Security.Claims;

namespace RealEstateAPI_Auth0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertiesController : ControllerBase
    {
        private readonly DBContextRealEstate _context;

        public PropertiesController(DBContextRealEstate context)
        {
            _context = context;
        }

        [HttpGet("PropertiesList")]
        [Authorize]
        public IActionResult GetPropertiesList(int categoryID)
        {
            //  User Exists
            var userExists = CheckIfUserExists();
            if (userExists == null) return StatusCode(StatusCodes.Status401Unauthorized);
            //  Get Results
            var propertyResults = _context.Properties.Where(i => i.CategoryID == categoryID);
            if (propertyResults == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            return Ok(propertyResults);
        }

        [HttpGet("PropertyDetail")]
        [Authorize]
        public IActionResult GetPropertyDetail(int propertyID)
        {
            //  User Exists
            var userExists = CheckIfUserExists();
            if (userExists == null) return StatusCode(StatusCodes.Status401Unauthorized);
            //  Get Results
            var propertyResults = _context.Properties.FirstOrDefault(i => i.PropertyID == propertyID);
            if (propertyResults == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            return Ok(propertyResults);
        }

        [HttpGet("TrendingProperties")]
        [Authorize]
        public IActionResult GetTrendingProperties()
        {
            //  User Exists
            var userExists = CheckIfUserExists();
            if (userExists == null) return StatusCode(StatusCodes.Status401Unauthorized);
            //  Get Results
            var propertyResults = _context.Properties.Where(i => i.IsTrending == true);
            if (propertyResults == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            return Ok(propertyResults);
        }

        [HttpGet("SearchProperties")]
        [Authorize]
        public IActionResult GetSearchProperties(string filter)
        {
            //  User Exists
            var userExists = CheckIfUserExists();
            if (userExists == null) return StatusCode(StatusCodes.Status401Unauthorized);
            //  Get Results
            var propertyResults = _context.Properties.Where(i => i.Address.Trim().ToLower().Contains(filter.Trim().ToLower()));
            if (propertyResults == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            return Ok(propertyResults);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] Property value)
        {
            try
            {
                var propertyResult = _context.Properties.FirstOrDefault(i => i.Name.ToLower().Trim() == value.Name.ToLower().Trim() && i.CategoryID == value.CategoryID);
                if (propertyResult == null)
                {
                    //  Property Found with Name
                    return StatusCode(StatusCodes.Status404NotFound, "A property with the same information already exists.");
                }
                else
                {
                    //  Claims
                    var userExists = CheckIfUserExists();
                    if (userExists == null) return StatusCode(StatusCodes.Status401Unauthorized);
                    //  Save Record
                    value.IsTrending = false;
                    value.UserID = userExists.UserID;
                    _context.Properties.Add(value);
                    _context.SaveChanges();
                    return StatusCode(StatusCodes.Status201Created);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Authorize]
        public IActionResult Put([FromBody] Property value)
        {
            try
            {
                var propertyResult = _context.Properties.FirstOrDefault(i => i.PropertyID != value.PropertyID && (i.Name.ToLower().Trim() == value.Name.ToLower().Trim() && i.CategoryID == value.CategoryID));
                if (propertyResult == null)
                {
                    //  Property Found with Name or CategoryID
                    return StatusCode(StatusCodes.Status404NotFound, "Property not found.");
                }
                else
                {
                    //  Claims
                    var userExists = CheckIfUserExists();
                    if (userExists == null) return StatusCode(StatusCodes.Status401Unauthorized);
                    //  Only the user can update their own record
                    if (userExists.UserID == value.UserID)
                    {
                        value.Name = propertyResult.Name;
                        value.Detail = propertyResult.Detail;
                        value.Price = propertyResult.Price;
                        value.Address = propertyResult.Address;
                        value.CategoryID = propertyResult.CategoryID;
                        value.IsTrending = false;
                        value.UserID = userExists.UserID;
                        _context.Properties.Add(value);
                        _context.SaveChanges();
                        return StatusCode(StatusCodes.Status200OK, "Property updated successfully.");
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status401Unauthorized);
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Authorize]
        public IActionResult Delete(int id)
        {
            try
            {
                var propertyResult = _context.Properties.FirstOrDefault(i => i.PropertyID != id);
                if (propertyResult == null)
                {
                    //  Property Found with Name or CategoryID
                    return StatusCode(StatusCodes.Status404NotFound, "Property not found.");
                }
                else
                {
                    //  Claims
                    var userExists = CheckIfUserExists();
                    if (userExists == null) return StatusCode(StatusCodes.Status401Unauthorized);
                    //  Only the user can delete their own record
                    if (userExists.UserID == id)
                    {
                        _context.Properties.Remove(propertyResult);
                        _context.SaveChanges();
                        return StatusCode(StatusCodes.Status200OK, "Property deleted successfully.");
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status401Unauthorized);
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private User? CheckIfUserExists()
        {
            var userEmail = User.Claims.FirstOrDefault(i => i.Type == ClaimTypes.Email)?.Value;
            var userExists = _context.Users.FirstOrDefault(u => u.Email.ToLower().Trim() == userEmail.ToLower().Trim());
            if (userExists == null) return null;
            return userExists;
        }
    }
}