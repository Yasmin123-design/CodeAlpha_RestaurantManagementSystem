using System.ComponentModel.DataAnnotations;

namespace RestaurantManagementSystem.Models
{
    public class MenuItem
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Size is required.")]
        [StringLength(50, ErrorMessage = "Size cannot exceed 50 characters.")]
        public string Size { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [RegularExpression(@"^\d{1}\.\d{2}$", ErrorMessage = "Price must be exactly 3 digits: 1 digits before the decimal and 2 digits after.")]
        public decimal Price { get; set; }
    }
}
