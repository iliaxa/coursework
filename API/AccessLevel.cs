using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace WpfApplicationEntity.API
{
    public class AccessLevel
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Level { get; set; }
        public virtual ICollection<Worker> Workers { get; set; }
    }
}
