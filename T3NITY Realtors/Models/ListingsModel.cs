using System.ComponentModel.DataAnnotations;

namespace T3NITY_Realtors.Models
{
    public class ListingsModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        [Display(Name = "Contact Info.")]
        public string ContactInfo { get; set; }
        public decimal Price { get; set; }

        [Display(Name = "ListingCategory")] 
        public string ListingCategory { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        [Display(Name = "Default Image")]
        public IFormFile DefaultImage { get; set; }
        public int UsersId { get; set; }
    }
}
