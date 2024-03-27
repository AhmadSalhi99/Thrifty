using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Thrifty.Models
{
    public class ItemImages
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }


        public string imageBase { get; set; } = "";
        

        public bool mainImage { get; set; } = false;


        [NotMapped]
        [DisplayName("Upload Image")]
        [Required]
        public IFormFile? ImageFile { get; set; }


        [ForeignKey(nameof(item))]
        public int itemId { get; set; }
        public Item? item { get; set; }
    }


   

}
