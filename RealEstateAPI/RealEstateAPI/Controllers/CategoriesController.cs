using Microsoft.AspNetCore.Mvc;
using RealEstateAPI.Models;

namespace RealEstateAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private List<Category> _categoryList = new List<Category>()
        {
            new Category(){ CategoryID = 0, Name = "Apartment", ImageURL = "apartment.png" },
            new Category(){ CategoryID = 1, Name = "House", ImageURL = "house.png" },
            new Category(){ CategoryID = 2, Name = "Flatlet", ImageURL = "flatlet.png" }
        };

        [HttpGet]
        public IEnumerable<Category> Get()
        {
            return _categoryList;
        }
    }
}