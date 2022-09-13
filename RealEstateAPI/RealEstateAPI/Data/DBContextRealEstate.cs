using Microsoft.EntityFrameworkCore;
using RealEstateAPI.Models;

namespace RealEstateAPI.Data
{
    public class DBContextRealEstate : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Property> Properties { get; set; }

        public DBContextRealEstate(DbContextOptions<DBContextRealEstate> options)
          : base(options)
        { }
    }
}