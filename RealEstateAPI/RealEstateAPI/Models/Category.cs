using System.ComponentModel.DataAnnotations;

namespace RealEstateAPI.Models
{
    public class Category
    {
        public int CategoryID { get; set; }
        [Required(ErrorMessage = "Category name is a required field.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Category image URL is a required field.")]
        public string ImageURL { get; set; }
        public string Description { get; set; }
    }
}