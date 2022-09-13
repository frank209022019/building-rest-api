using Microsoft.EntityFrameworkCore;
using QuotesAPI.Models;

namespace RealEstateAPI.Data
{
    public class DBContextQuote : DbContext
    {
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<QuoteType> QuoteTypes { get; set; }

        public DBContextQuote(DbContextOptions<DBContextQuote> options)
          : base(options)
        { }
    }
}