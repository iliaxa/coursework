using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace WpfApplicationEntity.API
{
    class Product
    {
        [Key] public int Id { get; set; }
        [Required] public string Name { get; set; }
        [Required] public string Storage_life { get; set; }
        public virtual ICollection<Dish_products> Dish_Products { get; set; }
        public virtual ICollection<Batch_of_products> Batch_Of_Products { get; set; }
    }
}
