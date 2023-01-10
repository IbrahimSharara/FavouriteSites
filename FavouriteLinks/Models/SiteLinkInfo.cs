using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace FavouriteLinks.Models
{
    public class SiteLinkInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        [ForeignKey("User")]
        [HiddenInput]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
