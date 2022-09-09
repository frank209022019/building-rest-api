using Microsoft.AspNetCore.Mvc;
using RealEstateAPI.Data;
using RealEstateAPI.Models;

namespace RealEstateAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoiesController : ControllerBase
    {
        private DBContextRealEstate _context = new DBContextRealEstate();

        // GET: api/<CategoiesController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var categories = _context.Categories;
                if (categories.Count() == 0)
                {
                    return NotFound("No categories found.");
                }
                else
                {
                    return Ok(categories);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/<CategoiesController>
        [HttpGet("[action]")]
        public IActionResult GetSortCategories()
        {
            try
            {
                var categories = _context.Categories;
                if (categories.Count() == 0)
                {
                    return NotFound("No categories found.");
                }
                else
                {
                    return Ok(categories.OrderByDescending(x => x.Name));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<CategoiesController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var category = _context.Categories.FirstOrDefault(i => i.CategoryID == id);
                if (category == null)
                {
                    return NotFound("Category not found.");
                }
                else
                {
                    return Ok(category);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/<CategoiesController>
        [HttpPost]
        public IActionResult Post([FromBody] Category value)
        {
            try
            {
                if (_context.Categories.Any(i => i.Name.ToLower().Trim() == value.Name.ToLower().Trim()))
                {
                    //  Category Found with Name
                    return StatusCode(StatusCodes.Status401Unauthorized, "A category with the same name already exists.");
                }
                else
                {
                    _context.Categories.Add(value);
                    _context.SaveChanges();
                    return StatusCode(StatusCodes.Status201Created);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<CategoiesController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Category value)
        {
            try
            {
                var category = _context.Categories.Find(id);
                if (category == null)
                {
                    //  Category Not Found
                    return NotFound("Category not found.");
                }
                else if (_context.Categories.Any(i => i.Name.ToLower().Trim() == value.Name.ToLower().Trim() && i.CategoryID != id))
                {
                    //  Category Found with Name
                    return StatusCode(StatusCodes.Status401Unauthorized, "A category with the same name already exists.");
                }
                else
                {
                    category.Name = value.Name;
                    category.ImageURL = value.ImageURL;
                    category.Description = value.Description;
                    _context.Categories.Update(category);
                    _context.SaveChanges();
                    return Ok("Record update successfully.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<CategoiesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var category = _context.Categories.Find(id);
                if (category == null)
                {
                    //  Category Not Found
                    return NotFound("Category not found.");
                }
                else
                {
                    _context.Categories.Remove(category);
                    _context.SaveChanges();
                    return Ok("Record deleted successfully.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}