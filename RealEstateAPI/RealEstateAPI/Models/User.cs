using System.ComponentModel.DataAnnotations;

namespace RealEstateAPI.Models
{
    public class User
    {
        public int UserID { get; set; }

        [Required(ErrorMessage = "Name is a required field.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is a required field.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone Number is a required field.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Password is a required field.")]
        public string Password { get; set; }

        #region Collections
        public ICollection<Property> Properties { get; set; }

        #endregion Collections
    }
}