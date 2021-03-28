using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace WpfApplicationEntity.API
{
    class Batch_of_products
    {
        [Key] public int Id { get; set; }
        [Required] public int Count { get; set; }
        [Required] public string Delivery_Date { get; set; }
    }
}
