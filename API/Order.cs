using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace WpfApplicationEntity.API
{
    public class Order
    {
        [Key] public int ID { get; set; }
        [Required] public DateTime Date { get; set; }
        [Required] public DateTime Time { get; set; }
        [Required] public string Place { get; set; }
        public virtual Transport Transport { get; set; }
        public virtual Worker Worker { get; set; }
        public virtual Client Client { get; set; }
        public virtual Order_Type Order_Type { get; set; }
        public virtual ICollection<Order_list> Order_Lists { get; set; }
    }
}
