using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantManagementSystem.Models
{
    public class Order
    {
        public int Id { get; set; }
        public List<OrderItem>? Items { get; set; }
        public DateTime OrderDate { get; set; }

        [RegularExpression(@"^\d{2}\.\d{2}$", ErrorMessage = "Price must be exactly 4 digits: 2 digits before the decimal and 2 digits after.")]
        private decimal _totalAmount;
        public decimal TotalAmount
        {
            get
            {
                return Items != null ? Items.Sum(item => item.Quantity * item.MenuItem.Price) : 0;
            }
            set
            {
                _totalAmount = value;
            }
        }

        [Required(ErrorMessage = "Order status is required.")]
        [RegularExpression("Pending|Completed|Cancelled", ErrorMessage = "StatusOrder must be either 'Pending', 'Completed', or 'Cancelled'.")]
        public string StatusOrder { get; set; }
    }
}
