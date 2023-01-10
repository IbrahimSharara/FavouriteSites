using FavouriteLinks.Models;
using Microsoft.EntityFrameworkCore;
namespace FavouriteLinks.Repository
{
    public class SiteRepository : ISiteRepository
    {
        public FavouriteSitesContext db { get; }
        public IImageRepository Image { get; }

        public SiteRepository(FavouriteSitesContext db , IImageRepository image)
        {
            this.db = db;
            Image = image;
        }


        public int Delete(int id)
        {
            SiteLinkInfo site = db.siteLinkInfos.SingleOrDefault(x => x.Id == id);
            db.Remove(site);
            return  db.SaveChanges();
        }

        public List<SiteLinkInfo> GetSiteViewModels(string userId)
        {
            return  db.siteLinkInfos.Where(s => s.UserId == userId).ToList();
        }

        public async Task<int> Insert(SiteViewModel site)
        {
            SiteLinkInfo sit = new SiteLinkInfo();
            sit.Link = site.Link;
            sit.Name = site.Name;
            sit.Description = site.Description;
            sit.UserId = site.UserId;
            if(site.ImageFile != null)
            {
                sit.Image = Image.AddnewImage(site.ImageFile);
            }
            await db.siteLinkInfos.AddAsync(sit);
            return await db.SaveChangesAsync();
        }

        public async Task<int> Update(SiteLinkInfo site)
        {
            SiteLinkInfo newSite = await db.siteLinkInfos.SingleOrDefaultAsync(s => s.Id == site.Id);
            newSite.Name = site.Name;
            newSite.Description = site.Description;
            newSite.Image = site.Image;
            newSite.Link = site.Link;
            return await db.SaveChangesAsync();
        }
    }
}
