using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Thrifty.Models
{
    public class User
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }


        [DisplayName("Full Name")]
        public string fullName { get; set; } = "";




        [DisplayName("Mobile Number")]
        public long mobileNumber { get; set; }


        [DisplayName("Email")]
        public string email { get; set; } = "";


        [DisplayName("Password")]
        public string password { get; set; } = "";


        [DisplayName("Confirm Password")]
        [Compare(nameof(password), ErrorMessage = "Password and confirm password are not identical")]
        [NotMapped]
        public string ConfirmPassword { get; set; } = "";



        [ForeignKey(nameof(city))]
        [DisplayName("City")]
        public int cityId { get; set; }
        public City? city { get; set; }




        [ForeignKey(nameof(role))]
        [DisplayName("Role")]
        public int roleId { get; set; }
        public Role? role { get; set; }


    }




    public class LoginUser
    {
        [DisplayName("Email")]
        [Required(ErrorMessage = "*")]
        public string email { get; set; } = "";


        [DisplayName("Password")]
        [Required(ErrorMessage = "*")]
        public string password { get; set; } = "";
    }




    public class RegisterUser
    {
        [DisplayName("Full Name")]
        [RegularExpression(@"^[a-zA-Z ]+$", ErrorMessage = "*")]
        [Required(ErrorMessage = "*")]
        public string fullName { get; set; } = "";


        [DisplayName("Email")]
        [RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", ErrorMessage = "*")]
        [Required(ErrorMessage = "*")]
        public string email { get; set; } = "";


        [DisplayName("City")]
        [Required(ErrorMessage = "*")]
        public int cityId { get; set; }


        [DisplayName("Mobile Number")]
        [Required(ErrorMessage = "*")]
        [RegularExpression(@"^((\+|00)962|0)7[789]\d{7}$", ErrorMessage = "*")]
        public string mobileNumber { get; set; } = "";


        [DisplayName("Password")]
        [Required(ErrorMessage = "*")]
        public string password { get; set; } = "";



        [DisplayName("Confirm Password")]
        [Required(ErrorMessage = "*")]
        [Compare(nameof(password), ErrorMessage = "*")]
        public string ConfirmPassword { get; set; } = "";
    }


    public class AccountInformation
    {
        public int id { get; set; }

        [DisplayName("Full Name")]
        [Required(ErrorMessage = "*")]
        public string fullName { get; set; } = "";


        [DisplayName("Mobile Number")]
        [Required(ErrorMessage = "*")]
        public long mobileNumber { get; set; }


        [DisplayName("Email")]
        [Required(ErrorMessage = "*")]
        public string email { get; set; } = "";


        [DisplayName("City")]
        [Required(ErrorMessage = "*")]
        public int cityId { get; set; }


        public List<OrderDetails>? orders { get; set; }
    }
}
