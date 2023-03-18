namespace T3NITY_Realtors.Models
{
    public class ListingsModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public string ContactInfo { get; set; }
        public decimal Price { get; set; }
        public string PerWhat { get; set; }
        public string Status { get; set; }
        public IFormFile DefaultImage { get; set; }
        public int UsersId { get; set; }
    }
}
