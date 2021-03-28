using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace WpfApplicationEntity.API
{
    class Worker
    {
        [Key] public int Id { get; set; }
        [Required] public string Surname { get; set; }
        [Required] public string Name { get; set; }
        [Required] public string Lastname { get; set; }
        [Required] public string Adress { get; set; }
        [Required] public string Phone_Number { get; set; }
        [Required] public string Driver_License { get; set; }
        [Required] public string Gender { get; set; }
        [Required] public string Login { get; set; }
        [Required] public string Password { get; set; }
        [Required] public string Birthday { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
