using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace WpfApplicationEntity.API
{
    public class Worker
    {
        [Key] public int ID { get; set; }
        [Required] public string Surname { get; set; }
        [Required] public string Name { get; set; }
        [Required] public string Lastname { get; set; }
        [Required] public string Adress { get; set; }
        [Required] public string Phone_Number { get; set; }
        [Required] public string Driver_License { get; set; }
        [Required] public bool Gender { get; set; }
        [Required] public string Login { get; set; }
        [Required] public string Password { get; set; }
        [Required] public DateTime Birthday { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual AccessLevel Access_Level { get; set; }
    }
}
