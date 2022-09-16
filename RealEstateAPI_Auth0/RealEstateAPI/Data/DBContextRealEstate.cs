using Microsoft.EntityFrameworkCore;
using RealEstateAPI_Auth0.Models;

namespace RealEstateAPI_Auth0.Data
{
    public class DBContextRealEstate : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<QuoteType> QuoteTypes { get; set; }

        public DBContextRealEstate(DbContextOptions<DBContextRealEstate> options)
          : base(options)
        { }
    }
}