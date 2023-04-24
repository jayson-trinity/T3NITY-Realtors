using T3NITY_Realtors.Entities;
using T3NITY_Realtors.Models;

namespace T3NITY_Realtors.Services.IServices
{
    public interface IListingsServices
    {
        bool ChangeStatusListing(int ListingId, string status);
        bool CreateListing(ListingsModel listingsModel, int userId);
        bool DeleteImage(int imageId, int listid);
        bool DeleteListing(int listingId);
        IEnumerable<ListingsViewModel> GetAllListings();
        ListingsViewModel GetListingById(int listingId);
        IEnumerable<ListingImages> GetListingImages(int listingId);
        IEnumerable<ListingsViewModel> GetListings(int userId);
        IEnumerable<ListingsViewModel> GetListings();
        bool UpdateListing(ListingsModel listingsModel, int userId);
        bool UpdateListingWithMessage(ListingsViewModel listingsModel);
        bool UploadImages(ImageUpload imageUpload);
    }
}
