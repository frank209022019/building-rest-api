using CoffeeShopAPI.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly DBContextCoffeeShop _context;

        public MenuController(DBContextCoffeeShop context)
        {
            _context = context;
        }

        [HttpGet("GetMenus")]
        public IActionResult Get()
        {
            try
            {
                var menus = _context.Menus.Include("SubMenus");
                if (menus.Count() == 0) return NotFound("No menus found.");
                return Ok(menus);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetMenuDetail")]
        public IActionResult GetMenuDetail(int id)
        {
            try
            {
                var menu = _context.Menus.Include("SubMenus").FirstOrDefault(i => i.ID == id);
                if (menu == null) return NotFound("Menu not found.");
                return Ok(menu);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetSubMenuDetail")]
        public IActionResult GetSubMenuDetail(int id)
        {
            try
            {
                var submenu = _context.SubMenus.FirstOrDefault(i => i.MenuID == id);
                if (submenu == null) return NotFound("SubMenu not found.");
                return Ok(submenu);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}