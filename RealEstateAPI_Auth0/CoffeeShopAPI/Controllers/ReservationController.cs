using CoffeeShopAPI.Database;
using CoffeeShopAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly DBContextCoffeeShop _context;

        public ReservationController(DBContextCoffeeShop context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Post([FromBody] Reservation value)
        {
            try
            {
                var propertyResult = _context.Reservations.FirstOrDefault(i => i.Time.ToLower().Trim() == value.Time.ToLower().Trim() && (i.Date.Day == value.Date.Day && i.Date.Month == value.Date.Month && i.Date.Year == value.Date.Year));
                if (propertyResult == null)
                {
                    //  Property Found with Name
                    return StatusCode(StatusCodes.Status404NotFound, "A reservation with the same information already exists.");
                }
                else
                {
                    _context.Reservations.Add(value);
                    _context.SaveChanges();
                    return StatusCode(StatusCodes.Status201Created);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}