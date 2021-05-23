using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace WpfApplicationEntity.API
{
    public class Transport
    {
        [Key] public int ID { get; set; }
        [Required] public string Name { get; set; }
        [Required] public string Number { get; set; }
        public virtual ICollection<Order> Orders { get; set; }

    }
}
