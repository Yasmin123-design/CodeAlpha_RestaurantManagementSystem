using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace RestaurantManagementSystem.Models
{
    public class ApplicationUser : IdentityUser
    {
        

        [Required(ErrorMessage = "address is required")]
        [StringLength(200, ErrorMessage = "adddres should not exceed 200 chars")]
        public string Address { get; set; }
    }
}
