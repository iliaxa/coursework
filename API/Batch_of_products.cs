using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace WpfApplicationEntity.API
{
    public class Batch_of_products
    {
        [Key] public int ID { get; set; }
        [Required] public int Count { get; set; }
        [Required] public DateTime Delivery_Date { get; set; }
        public virtual Product Product { get; set; }
    }
}
