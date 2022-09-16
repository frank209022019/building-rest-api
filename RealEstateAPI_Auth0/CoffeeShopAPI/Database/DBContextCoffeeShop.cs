using CoffeeShopAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShopAPI.Database
{
    public class DBContextCoffeeShop : DbContext
    {
        public DbSet<Menu> Menus { get; set; }
        public DbSet<SubMenu> SubMenus { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        public DBContextCoffeeShop(DbContextOptions<DBContextCoffeeShop> options)
          : base(options)
        { }
    }
}