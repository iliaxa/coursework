using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace WpfApplicationEntity.API
{
    class Order_list
    {
        [Key] public int ID { get; set; }
        [Required] public string Count { get; set; }
    }
}
