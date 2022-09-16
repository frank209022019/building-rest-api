using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CoffeeShopAPI.Models
{
    public class SubMenu
    {
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public string Image { get; set; }

        #region FK

        public int MenuID { get; set; }

        [JsonIgnore]
        public Menu Menu { get; set; }

        #endregion FK
    }
}