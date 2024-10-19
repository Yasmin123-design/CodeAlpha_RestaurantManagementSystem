using System.ComponentModel.DataAnnotations;

namespace RestaurantManagementSystem.Models
{
    public class Table
    {
        public int Id { get; set; }
        [Required]
        public int TableNumber { get; set; }
        [Range(3,8)]
        public int NoOfChairs { get; set; }
        [RegularExpression("Available|Unavailable", ErrorMessage = "Status must be either 'Available' or 'Unavailable'.")]
        public string Status { get; set; }

    }
}
