namespace T3NITY_Realtors.Models
{
    public class ImageUpload
    {
        public int ListingsId { get; set; }
        public IFormFile File { get; set; }
        public bool IsDefault { get; set; }

    }
}
