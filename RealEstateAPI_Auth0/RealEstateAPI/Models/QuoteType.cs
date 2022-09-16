namespace RealEstateAPI_Auth0.Models
{
    public class QuoteType
    {
        public int Id { get; set; }
        public string Description { get; set; }

        #region Collections

        public ICollection<Quote> Quotes { get; set; }

        #endregion Collections
    }
}
