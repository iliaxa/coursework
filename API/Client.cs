using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace WpfApplicationEntity.API
{
    public class Client
    {
        [Key] public int ID { get; set; }
        [Required] public string Surname { get; set; }
        [Required] public string Name { get; set; }
        [Required] public string Lastname { get; set; }
        [Required] public string Phone_Number { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
