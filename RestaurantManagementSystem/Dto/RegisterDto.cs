using System.ComponentModel.DataAnnotations;

namespace RestaurantManagementSystem.Dto
{
    public class RegisterDto
    {
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string Password { get; set;}
        public string ConfirmPassword { get; set; }
        public string Address { get; set; }
    }
}
