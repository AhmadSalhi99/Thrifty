using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Thrifty.Models
{
    public class OrderDetails
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string OrderNumber { get; set; } = "";

        public decimal Price { get; set; }


        [DisplayName("Order Date")]
        public DateTime OrderDate { get; set; }


        [ForeignKey(nameof(Item))]
        public int ItemId { get; set; }
        public Item? Item { get; set; }



        [ForeignKey(nameof(User))]
        public int? buyerId { get; set; }
        public User? User { get; set; }



        [ForeignKey(nameof(OrderStatus))]
        public int statusId { get; set; }
        public OrderStatus? OrderStatus { get; set; }
    }
}
