using System.ComponentModel.DataAnnotations;

namespace CoffeeShopAPI.Models
{
    public class Menu
    {
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Image { get; set; }

        #region Collections

        public ICollection<SubMenu> SubMenus { get; set; }

        #endregion Collections
    }
}