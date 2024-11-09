using E_CommerceApp.Interfaces;
using E_CommerceApp.Models;

namespace E_CommerceApp.Service
{
    public class ProductService : IEntityService
    {
        #region public Members

        public object? Create(object Entity, IFormFile? file)
        {
            if (typeof(Product).IsAssignableFrom(Entity.GetType()))
            {
                var entity = (Product)Entity;
                var path = string.Empty;

                if (!string.IsNullOrWhiteSpace(entity.ImageUrl))
                {
                    DeleteFile(entity.ImageUrl);
                }

                if (file != null) { 
                var extention = GetFileExtention(file);
                    if (IsImage(extention))
                    {
                        path = GetFilePath(file);
                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            file.CopyToAsync(stream);
                        }
                    }
                }
                entity.ImageUrl = path;
                return entity;
            }
            return null;

        }

        public void Delete(object Entity)
        {
            Product entity = (Product)Entity;
            if (!string.IsNullOrWhiteSpace(entity.ImageUrl))
            {
                File.Delete(entity.ImageUrl);
            }
        }
        #endregion

        #region private Members
        private static string GetFileExtention(IFormFile file)
        {
            return "." + file.FileName.Split('.')[^1];

        }
        private static bool IsImage(string extention)
        {
            return extention.Equals(".png") || extention.Equals(".jpg");

        }
        private static string GetFilePath(IFormFile file)
        {
            string fileName = DateTime.Now.Ticks + file.FileName;

            var pathBuilt = Path.Combine(Directory.GetCurrentDirectory(), "Resources\\Files");
            if (!Directory.Exists(pathBuilt))
            {
                Directory.CreateDirectory(pathBuilt);
            }
            return Path.Combine(Directory.GetCurrentDirectory(), "Resources\\Files", fileName);

        }
        private static void DeleteFile(string path)
        {
            File.Delete(path);
        }
        #endregion
    }
}
