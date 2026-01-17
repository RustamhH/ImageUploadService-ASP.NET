using ImageUploadService.Context;
using Microsoft.EntityFrameworkCore;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;

namespace ImageUploadService.Services
{
    public class ImageService
    {
        private readonly AppDBContext _appDBContext;
        private readonly IWebHostEnvironment _webHostEnvironment; 

        public ImageService(AppDBContext appDBContext, IWebHostEnvironment webHostEnvironment)
        {
            _appDBContext = appDBContext;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<bool> UploadFileAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return false;

            var allowedTypes = new[] { "image/png", "image/jpeg", "image/gif", "image/jpg" };
            if (!allowedTypes.Contains(file.ContentType))
                return false;

            if (file.Length > 5 * 1024 * 1024)
                return false;

            var uploadsPath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
            Directory.CreateDirectory(uploadsPath);

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(uploadsPath, fileName);

            await using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            using var image = await Image.LoadAsync(filePath);


            image.Mutate(x => x.Resize(new ResizeOptions
            {
                Mode = ResizeMode.Max,
                Size = new Size(1200, 0)
            }));

            var encoder = new JpegEncoder
            {
                Quality = 75
            };

            await image.SaveAsync(filePath, encoder);

            var entity = new Models.Image
            {
                FileName = fileName,
                FilePath = $"/uploads/{fileName}",
                UploadTime = DateTime.Now,
                Width=image.Width, Height=image.Height,
                Format = image.Metadata.DecodedImageFormat?.Name,
                FileSize = file.Length
            };

            _appDBContext.Images.Add(entity);
            await _appDBContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteImageAsync(string id)
        {
            var image = await _appDBContext.Images.FindAsync(id);
            if (image == null)
                return false;

            var physicalPath = Path.Combine(_webHostEnvironment.WebRootPath, image.FilePath.TrimStart('/'));
            if (File.Exists(physicalPath))
                File.Delete(physicalPath);

            _appDBContext.Images.Remove(image);
            await _appDBContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<Models.Image>> GetAllImages()
        {
            return await _appDBContext.Images.ToListAsync();
        }

    }
}
