using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace WpfApplicationEntity.API
{
    public class Order_Type
    {
        [Key]
        public int ID { get; set; }
        [Required] public string Name { get; set; }
        [Required] public string Description { get; set; }
        [Required] public string Status { get; set; }
        public virtual ICollection<Order> Orders { get; set; }

    }
}
