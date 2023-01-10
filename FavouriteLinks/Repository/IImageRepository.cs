namespace FavouriteLinks.Repository
{
    public interface IImageRepository
    {
        string AddnewImage(IFormFile file);
        bool delete(string image);
        string update(string image, IFormFile file);
    }
}
