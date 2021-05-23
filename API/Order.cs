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
        public Transport Transport { get; set; }
        public Worker Worker { get; set; }
        public Client Client { get; set; }
        public Order_Type Order_Type { get; set; }
        public ICollection<Order_list> Order_Lists{ get; set; }
    }
}
