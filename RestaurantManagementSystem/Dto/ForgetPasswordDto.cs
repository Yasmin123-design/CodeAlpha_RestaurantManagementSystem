using System.ComponentModel.DataAnnotations;

namespace RestaurantManagementSystem.Dto
{
    public class ForgetPasswordDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
