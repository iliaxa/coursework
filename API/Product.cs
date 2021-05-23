using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace WpfApplicationEntity.API
{
    public class Product
    {
        [Key] public int ID { get; set; }
        [Required] public string Name { get; set; }
        [Required] public string Storage_life { get; set; }
        public virtual Product_Type Product_Type { get; set; }
        public virtual ICollection<Dish_products> Dish_Products { get; set; }
        public virtual ICollection<Batch_of_products> Batch_Of_Products { get; set; }
    }
}
