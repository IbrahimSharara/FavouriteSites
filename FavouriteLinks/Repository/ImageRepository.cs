namespace FavouriteLinks.Repository
{
    public class ImageRepository : IImageRepository
    {
        public string AddnewImage(IFormFile file)
        {
            string image = file.FileName;
            string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot" , "images" , image);
            using(Stream stream = File.Create(imagePath))
            {
                file.CopyTo(stream);
            }
            return image;
        }

        public bool delete(string image)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory() , "wwwroot" , "images" , image);
            FileInfo fileInfo = new FileInfo(filePath);
            if (fileInfo.Exists)
            {
                fileInfo.Delete();
                return true;
            }
            else
                return false;
        }

        public string update(string image , IFormFile file)
        {
            if (delete(image))
            {
                return AddnewImage(file);
            }
            else
                return "Error";
        }
    }
}
