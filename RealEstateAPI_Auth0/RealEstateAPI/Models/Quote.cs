using System.Text.Json.Serialization;

namespace RealEstateAPI_Auth0.Models
{
    public class Quote
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }

        #region FK

        public int QuoteTypeID { get; set; }

        [JsonIgnore]
        public QuoteType QuoteType { get; set; }

        #endregion FK
    }
}