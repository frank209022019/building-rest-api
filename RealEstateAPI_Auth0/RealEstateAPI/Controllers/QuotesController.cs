using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstateAPI_Auth0.Data;
using RealEstateAPI_Auth0.Models;

namespace RealEstateAPI_Auth0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class QuotesController : ControllerBase
    {
        private readonly DBContextRealEstate _context;

        public QuotesController(DBContextRealEstate context)
        {
            _context = context;
        }

        #region GET

        [HttpGet]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Client)]
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

        [HttpGet("GetDetail")]
        public IActionResult GetDetail(int id)
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

        [HttpGet("GetFiltered")]
        public IActionResult GetFiltered()
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

        [HttpGet("GetSorted")]
        public IActionResult GetSorted(string sort)
        {
            try
            {
                IQueryable<Quote> quotes;
                switch (sort)
                {
                    case "asc":
                        quotes = _context.Quotes.OrderByDescending(i => i.DateCreated);
                        break;

                    case "desc":
                        quotes = _context.Quotes.OrderBy(i => i.DateCreated);
                        break;

                    default:
                        quotes = _context.Quotes;
                        break;
                }

                if (quotes.Count() == 0) return NotFound("No quotes found.");
                return Ok(quotes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetPaged")]
        public IActionResult GetPaged(int pageNumber, int pageSize)
        {
            try
            {
                var quotes = _context.Quotes;
                //  Skip the previous pages and take the next dataset
                if (quotes.Count() == 0) return NotFound("No quotes found.");
                return Ok(quotes.Skip((pageNumber - 1) * pageSize).Take(pageSize));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #endregion GET

        #region POST-PUT-DELETE

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

        #endregion POST-PUT-DELETE
    }
}