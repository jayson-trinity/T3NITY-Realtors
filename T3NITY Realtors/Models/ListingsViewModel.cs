using T3NITY_Realtors.Entities;

namespace T3NITY_Realtors.Models
{
    public class ListingsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public string ContactInfo { get; set; }
        public decimal Price { get; set; }
        public string PerWhat { get; set; }
        public string Status { get; set; }
        public string AdminMessage { get; set; }
        public ListingImages DefaultImages { get; set; }
        public IEnumerable<ListingImages> ListingImages { get; set; }

        public static explicit operator ListingsViewModel(Listings v)
        {

            return new ListingsViewModel()
            {
                Address = v.Address,
                AdminMessage = v.AdminMessage,
                ContactInfo = v.ContactInfo,
                Description = v.Description,
                Id = v.Id,
                Name = v.Name,
                PerWhat = v.PerWhat,
                Price = v.Price,
                Status = v.Status
            };
        }
    }
}
