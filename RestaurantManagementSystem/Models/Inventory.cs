using System.ComponentModel.DataAnnotations;

namespace RestaurantManagementSystem.Models
{
    public class Inventory
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="name is required")]
        public string ItemName { get; set; }

        [Required]
        [Range(1,int.MaxValue)]
        public int Quantity { get; set; }
        public int MinimumQuantity { get; set; }
    }
}
