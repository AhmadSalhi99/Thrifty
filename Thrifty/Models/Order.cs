using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Thrifty.Models
{

    public class Order
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DisplayName("Order Number")]
        public string OrderNumber { get; set; } = "";

        [DisplayName("Toral Price")]
        public decimal ToralPrice { get; set; }

        [DisplayName("Order Date")]
        public DateTime OrderDate { get; set; }


        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public User? User { get; set; }


        [ForeignKey(nameof(OrderStatus))]
        public int statusId { get; set; }
        public OrderStatus? OrderStatus { get; set; }

    }
}
