using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace T3NITY_Realtors.Entities
{
    public class Listings : BaseEntity
    {
        
        public string Name { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public string ContactInfo { get; set; }
        public decimal Price { get; set; }
        public ListingCategory ListingCategory { get; set; }
        public Status Status { get; set; }
        public bool Available { get; set; }
        public Type Type { get; set; }
        public string AdminMessage { get; set; }
        public int UsersId { get; set; }
        public Users Users { get; set; }
    }


    public enum ListingCategory
    {
        [Display(Name = "Self Contain")]
        Self_Contain,
        [Display(Name = "Mini Flat")]
        Mini_Flat,
        [Display(Name = "2 Bedroom Apartment")]
        Bedroom2_Apartment,
        [Display(Name = "3 Bedroom Apartment")]
        Bedroom3_Apartment,
        Bungalow,
        Duplex,
    }

    public enum Status
    {
        Pending,
        Approved,
        Faked,
        Declined,
        Suspended
    }
    public enum Type
    {
        Rent,
        Sale,
        Lease

    }

}
