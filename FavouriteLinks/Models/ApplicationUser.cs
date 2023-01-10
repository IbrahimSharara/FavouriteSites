using Microsoft.AspNetCore.Identity;

namespace FavouriteLinks.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Image { get; set; }
        public string Address { get; set; }
    }
}
