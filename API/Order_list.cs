using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace WpfApplicationEntity.API
{
    public class Order_list
    {
        [Key] public int ID { get; set; }
        [Required] public string Count { get; set; }
        [Required] public Order Order { get; set; }
        [Required] public Dish Dish { get; set; }
    }
}
