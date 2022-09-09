using System.ComponentModel.DataAnnotations;

namespace RealEstateAPI.Models
{
    public class Property
    {
        public int PropertyID { get; set; }

        [Required(ErrorMessage = "Name is a required field.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Detail is a required field.")]
        public string Detail { get; set; }

        [Required(ErrorMessage = "Address is a required field.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Image URL is a required field.")]
        public string ImageURL { get; set; }

        [Required(ErrorMessage = "Price is a required field.")]
        public double Price { get; set; }

        public bool IsTrending { get; set; }

        #region FK

        public int CategoryID { get; set; }
        public Category Category { get; set; }
        public int UserID { get; set; }
        public User User { get; set; } = new User();

        #endregion FK
    }
}