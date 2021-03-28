using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace WpfApplicationEntity.API
{
    class Order
    {
        [Key] public int ID { get; set; }
        [Required] public string Date { get; set; }
        [Required] public string Time { get; set; }
        [Required] public string Place { get; set; }
        public ICollection<Order_list> Order_Lists{ get; set; }
    }
}
