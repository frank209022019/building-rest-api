using Microsoft.AspNetCore.Mvc;
using QuotesAPI.Models;
using RealEstateAPI.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QuotesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuotesController : ControllerBase
    {
        private readonly DBContextQuote _context;

        public QuotesController(DBContextQuote context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var quotes = _context.Quotes;
                if (quotes.Count() == 0) return NotFound("No quotes found.");
                return Ok(quotes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("[action]")]
        public IActionResult GetSortQuotes()
        {
            try
            {
                var quotes = _context.Quotes;
                if (quotes.Count() == 0) return NotFound("No quotes found.");

                return Ok(quotes.OrderByDescending(x => x.Title));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var quote = _context.Quotes.FirstOrDefault(i => i.Id == id);
                if (quote == null) return NotFound("Quote not found.");
                return Ok(quote);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] Quote value)
        {
            try
            {
                if (_context.Quotes.Any(i => i.Title.ToLower().Trim() == value.Title.ToLower().Trim()))
                {
                    //  Quote Found with Name
                    return StatusCode(StatusCodes.Status404NotFound, "A quote with the same information already exists.");
                }
                else
                {
                    _context.Quotes.Add(value);
                    _context.SaveChanges();
                    return StatusCode(StatusCodes.Status201Created);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Quote value)
        {
            try
            {
                var quote = _context.Quotes.Find(id);
                if (quote == null)
                {
                    //  Quote Not Found
                    return NotFound("Quote not found.");
                }
                else if (_context.Quotes.Any(i => i.Title.ToLower().Trim() == value.Title.ToLower().Trim() && i.Id != id))
                {
                    //  Quote Found with Name
                    return StatusCode(StatusCodes.Status401Unauthorized, "A quote with the same name already exists.");
                }
                else
                {
                    quote.Title = value.Title;
                    quote.Author = value.Author;
                    quote.Description = value.Description;
                    quote.DateCreated = value.DateCreated;
                    quote.QuoteTypeID = value.QuoteTypeID;
                    _context.Quotes.Update(quote);
                    _context.SaveChanges();
                    return Ok("Record update successfully.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var quote = _context.Quotes.Find(id);
                if (quote == null)
                {
                    //  Quote Not Found
                    return NotFound("Quote not found.");
                }
                else
                {
                    _context.Quotes.Remove(quote);
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
