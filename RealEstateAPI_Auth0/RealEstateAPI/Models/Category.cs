using System.ComponentModel.DataAnnotations;

namespace RealEstateAPI_Auth0.Models
{
    public class Category
    {
        public int CategoryID { get; set; }

        [Required(ErrorMessage = "Name is a required field.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Image URL is a required field.")]
        public string ImageURL { get; set; }

        public string Description { get; set; }

        #region Collections

        public ICollection<Property> Properties { get; set; }

        #endregion Collections
    }
}