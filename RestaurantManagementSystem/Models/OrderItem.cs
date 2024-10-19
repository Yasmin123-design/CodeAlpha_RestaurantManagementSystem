using System.ComponentModel.DataAnnotations;

namespace RestaurantManagementSystem.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="name is required")]
        public int Quantity { get; set; }
        public int MenuItemId { get; set; }
        public int OrderId { get; set; }
        public MenuItem? MenuItem { get; set; }
        public Order? Order { get; set; }
        
    }
}
