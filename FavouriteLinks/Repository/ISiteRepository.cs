using FavouriteLinks.Models;

namespace FavouriteLinks.Repository
{
    public interface ISiteRepository
    {
        List<SiteLinkInfo> GetSiteViewModels(string userId);
        Task<int> Update(SiteLinkInfo site);
        int Delete(int id);
        Task<int> Insert(SiteViewModel site);

    }
}
