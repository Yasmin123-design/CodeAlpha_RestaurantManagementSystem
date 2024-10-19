using System.Security.Claims;

namespace RestaurantManagementSystem.Models
{
    public class Reversation
    {
        public int Id { get; set; }
        public int TableId { get; set; }
        public string? ApplicationUserId { get; set; } 
        public DateTime ReversationDate { get; set; }
        public Table? Table { get; set; }
        public ApplicationUser? ApplicationUser { get; set; }
    }
}
