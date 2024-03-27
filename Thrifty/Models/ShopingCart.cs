using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Thrifty.Models
{
    public class ShopingCart
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string CartNumber { get; set; } = "";


        [ForeignKey(nameof(Item))]
        public int ItemId { get; set; }
        public Item Item { get; set; }


        public decimal Price { get; set; }
    }
}
