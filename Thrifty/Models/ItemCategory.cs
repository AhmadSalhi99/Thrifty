using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Thrifty.Models
{
    public class ItemCategory
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }


        [Required]
        [DisplayName("Name")]
        public string name { get; set; } = "";


        public virtual List<Item>? Items { get; set; }
    }
}
