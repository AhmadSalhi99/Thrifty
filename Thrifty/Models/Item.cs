using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace Thrifty.Models
{

    public class Item
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }


        [DisplayName("Name")]
        [Required]
        public string name { get; set; } = "";


        [DisplayName("Description")]
        [Required]
        public string description { get; set; } = "";


        [DisplayName("Price")]
        [Required]
        public decimal price { get; set; }


        [DisplayName("Color")]
        [Required]
        public string color { get; set; } = "";


        [DisplayName("Size")]
        [Required]
        public string size { get; set; } = "";


        [DisplayName("Made in")]
        public string MadeIn { get; set; } = "";


        [DisplayName("Item State")]
        public long ItemState { get; set; }



        [DisplayName("Out Of Stock")]
        [Required]
        public bool outOfStock { get; set; } = false;


        [ForeignKey(nameof(User))]
        [DisplayName("Seller")]
        public int userId { get; set; }
        public User? User { get; set; }



        [ForeignKey(nameof(ItemType))]
        [DisplayName("Item Type")]
        public int ItemTypeId { get; set; }
        public ItemCategory? ItemType { get; set; }


        public virtual ICollection<ItemImages>? itemImages { get; set; }




        [NotMapped]
        public ItemImages? mainImage { get; set; }
    }



    public class CartItem
    {
        public List<Item>? items { get; set; }

        public decimal totalPrice { get; set; }

        public bool someItemSold { get; set; }
    }
}
