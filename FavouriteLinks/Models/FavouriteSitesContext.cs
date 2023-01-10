using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FavouriteLinks.Models
{
    public class FavouriteSitesContext :IdentityDbContext<ApplicationUser>
    {
        public FavouriteSitesContext()
        {

        }
        public FavouriteSitesContext(DbContextOptions<FavouriteSitesContext> options) : base(options)
        {

        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=.;Database=FavouriteSites;Trusted_Connection=True;");
        //}

        public DbSet<SiteLinkInfo>  siteLinkInfos { set; get; }
    }
}
