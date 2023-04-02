using System.ComponentModel.DataAnnotations;

namespace T3NITY_Realtors.Models
{
    public class ListingsModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [Display(Name = "Contact Info.")]
        public string ContactInfo { get; set; }
        [Required]
        public decimal Price { get; set; }

        [Required]
        [Display(Name = "ListingCategory")]
        public string ListingCategory { get; set; }
        public string Status { get; set; } = String.Empty;
        [Required]
        public string Type { get; set; }
        [Display(Name = "Default Image")]
        [Required]
        public IFormFile DefaultImage { get; set; }
        public bool Available { get; set; }
        public int UsersId { get; set; }
    }
}
