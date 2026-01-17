namespace ImageUploadService.Models
{
    public class Image:BaseEntity
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public DateTime UploadTime { get; set; } = DateTime.Now;
        public int Width { get; set; }
        public int Height { get; set; }
        public long FileSize { get; set; }
        public string Format { get; set; }
    }
}
