using T3NITY_Realtors.Entities;
using T3NITY_Realtors.Extentions;
using T3NITY_Realtors.Models;
using T3NITY_Realtors.Repository.IRepository;
using T3NITY_Realtors.Services.IServices;

namespace T3NITY_Realtors.Services
{
    public class ListingsServices : IListingsServices
    {
        protected IDbOperations _DbOperations;
        public ListingsServices(IDbOperations dbOperations)
        {
            _DbOperations = dbOperations;
        }

        public bool CreateListing(ListingsModel listingsModel, int userId)
        {
            var tranz = _DbOperations.GetDbContext();

            try
            {
                if (listingsModel != null)
                {
                    tranz.BeginTransaction();

                    Listings listing = new()
                    {
                        Address = listingsModel.Address,
                        ContactInfo = listingsModel.ContactInfo,
                        Description = listingsModel.Description,
                        Name = listingsModel.Name,
                        ListingCategory = (ListingCategory)Enum.Parse(typeof(ListingCategory), listingsModel.ListingCategory),
                        Price = listingsModel.Price,
                        Status = Status.Processing,
                        UsersId = userId
                    };

                    var dbListings = _DbOperations.ListingsRepository().Add(listing); ;

                    ListingImages images = new()
                    {
                        FileByte = listingsModel.DefaultImage.GetBytes(),
                        FileName = listingsModel.DefaultImage.FileName,
                        Extension = System.IO.Path.GetExtension(listingsModel.DefaultImage.FileName),
                        ListingsId = dbListings.Id,
                        IsDefault = true
                    };
                    var dbImages1 = _DbOperations.ListingImagedRepository().Add(images);

                    tranz.CommitTransaction();
                    return true;
                }
            }
            catch (Exception e)
            {
                tranz.RollbackTransaction();
                throw new Exception(e.Message);
            }

            return false;
        }

        public IEnumerable<ListingsViewModel> GetListings(int userId)
        {
            try
            {

                var _listings = _DbOperations.ListingsRepository().GetAll().Where(l => l.UsersId == userId).Select(l => (ListingsViewModel)l).ToList();
                foreach (var list in _listings)
                {
                    list.DefaultImages = _DbOperations.ListingImagedRepository().Find(i => i.ListingsId == list.Id && i.IsDefault);
                }
                return _listings;
            }
            catch (Exception e)
            {

                throw;
            }

            return null;
        }

        public IEnumerable<ListingsViewModel> GetAllListings()
        {
            try
            {

                var _listings = _DbOperations.ListingsRepository().GetAll().Where(l => l.Status == Status.Approved).Select(l => (ListingsViewModel)l).ToList();
                foreach (var list in _listings)
                {
                    list.DefaultImages = _DbOperations.ListingImagedRepository().Find(i => i.ListingsId == list.Id && i.IsDefault);
                }
                return _listings;
            }
            catch (Exception e)
            {

                throw;
            }

            return null;
        }

        public ListingsViewModel GetListingById(int listingId)
        {
            try
            {

                if (listingId > 0)
                {
                    var _listing = (ListingsViewModel)_DbOperations.ListingsRepository().Find(l => l.Id == listingId);

                    _listing.ListingImages = _DbOperations.ListingImagedRepository().GetAll().Where(i => i.ListingsId == _listing.Id);

                    return _listing;
                }
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
            return null;
        }

        public bool UploadImages(ImageUpload imageUpload)
        {

            try
            {
                if (imageUpload != null)
                {
                    var dbListings = _DbOperations.ListingsRepository().Find(l => l.Id == imageUpload.ListingsId) ?? throw new Exception("Invalid Listing ID");

                    ListingImages images = new()
                    {
                        FileByte = imageUpload.File.GetBytes(),
                        FileName = imageUpload.File.FileName,
                        Extension = System.IO.Path.GetExtension(imageUpload.File.FileName),
                        ListingsId = dbListings.Id,
                        IsDefault = imageUpload.IsDefault
                    };
                    var dbImages1 = _DbOperations.ListingImagedRepository().Add(images);

                    return true;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return false;
        }

        public IEnumerable<ListingImages> GetListingImages(int listingId)
        {
            try
            {
                if (listingId > 0)
                {
                    return _DbOperations.ListingImagedRepository().GetAll().Where(im => im.ListingsId == listingId).ToList();
                }
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
            return null;
        }

        public bool DeletListing(int listingId)
        {
            var tranz = _DbOperations.GetDbContext();

            try
            {
                if (listingId > 0)
                {
                    tranz.BeginTransaction();
                    var dbListingImages = _DbOperations.ListingImagedRepository().GetAll().Where(im => im.ListingsId == listingId).ToList();
                    foreach (var img in dbListingImages)
                    {
                        _DbOperations.ListingImagedRepository().Delete(img);
                    }
                    var dbListings = _DbOperations.ListingsRepository().Find(li => li.Id == listingId);
                    _DbOperations.ListingsRepository().Delete(dbListings);
                    tranz.CommitTransaction();
                    return true;
                }
            }
            catch (Exception e)
            {
                tranz.RollbackTransaction();
                throw new Exception(e.Message);
            }

            return false;
        }


    }
}
