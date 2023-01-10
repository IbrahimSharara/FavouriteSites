using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace FavouriteLinks.Models
{
    public class SiteViewModel
    {
        [Required]
        [MinLength(5 , ErrorMessage ="Please enter Valid Name")]
        public string Name { get; set; }
        [Required]
        [MinLength(5, ErrorMessage = "Please enter Valid Link")]
        public string Link { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public IFormFile? ImageFile { get; set; }
        public string? UserId { get; set; }
    }
}
