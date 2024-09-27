namespace GreenSeedCRE.Models
{
    public class Image
    {
        public int ImageId { get; set; } // Unique identifier for the image
        public string? FileName { get; set; } // Name of the image file
        public string? ContentType { get; set; } // MIME type of the image (e.g., "image/jpeg")
        public byte[]? Data { get; set; } // Binary data of the image
        public DateTime UploadDate { get; set; } // Date when the image was uploaded
    }
}
