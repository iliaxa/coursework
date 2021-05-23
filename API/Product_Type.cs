using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace WpfApplicationEntity.API
{
    public class Product_Type
    {
        [Key] public int ID { get; set; }
        [Required] public string Name { get; set; }
        [Required] public string Description { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
