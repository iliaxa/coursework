using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace WpfApplicationEntity.API
{
    class Dish
    {
        [Key] public int ID { get; set; }
        [Required] public string Name { get; set; }
        [Required] public double Price { get; set; }
        [Required] public double Weight { get; set; }
        [Required] public string Composition { get; set; }
        public virtual ICollection<Dish_products> Dish_Products{ get; set; }
        public virtual ICollection<Order_list> Order_Lists{ get; set; }
    }
}
