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
                        Status = Status.Pending,
                        Type = (Entities.Type)Enum.Parse(typeof(Entities.Type), listingsModel.Type),
                        UsersId = userId,
                        AdminMessage = String.Empty,
                        Available = listingsModel.Available
                    };

                    var dbListings = _DbOperations.ListingsRepository().Add(listing);

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
        public bool UpdateListing(ListingsModel listingsModel, int userId)
        {
            var tranz = _DbOperations.GetDbContext();

            try
            {
                if (listingsModel != null)
                {
                    tranz.BeginTransaction();

                    var dbListings = _DbOperations.ListingsRepository().Find(l => l.Id == listingsModel.Id);
                    dbListings.Address = listingsModel.Address;
                    dbListings.ContactInfo = listingsModel.ContactInfo;
                    dbListings.Description = listingsModel.Description;
                    dbListings.Name = listingsModel.Name;
                    dbListings.Status = Status.Pending;
                    dbListings.ListingCategory = (ListingCategory)Enum.Parse(typeof(ListingCategory), listingsModel.ListingCategory);
                    dbListings.Price = listingsModel.Price;
                    dbListings.Type = (Entities.Type)Enum.Parse(typeof(Entities.Type), listingsModel.Type);
                    dbListings.Available = listingsModel.Available;

                    _DbOperations.ListingsRepository().Update(dbListings, dbListings.Id);

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

                var _listings = _DbOperations.ListingsRepository().GetAll().Where(l => l.UsersId == userId).OrderBy(l => l.CreatedAt).Select(l => (ListingsViewModel)l).ToList();
                //foreach (var list in _listings)
                //{
                //    list.DefaultImages = _DbOperations.ListingImagedRepository().Find(i => i.ListingsId == list.Id && i.IsDefault);
                //}
                return _listings;
            }
            catch (Exception e)
            {

                throw;
            }

            return null;
        }
        public IEnumerable<ListingsViewModel> GetListings()
        {
            try
            {

                var _listings = _DbOperations.ListingsRepository().GetAll().OrderBy(l => l.CreatedAt).Select(l => (ListingsViewModel)l).ToList();
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

                var _listings = _DbOperations.ListingsRepository().GetAll().Where(l => l.Status == Status.Approved && l.Available).OrderBy(l => l.CreatedAt).Select(l => (ListingsViewModel)l).ToList();
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

                    _listing.ListingImages = _DbOperations.ListingImagedRepository().GetAll().Where(i => i.ListingsId == _listing.Id).ToList();

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
                    var tranz = _DbOperations.GetDbContext();
                    tranz.BeginTransaction();
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
                    dbListings.Status = Status.Pending;
                    _DbOperations.ListingsRepository().Add(dbListings);
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

        public bool ChangeStatusListing(int ListingId, string status)
        {
            var tranz = _DbOperations.GetDbContext();

            try
            {
                if (ListingId > 0)
                {
                    tranz.BeginTransaction();

                    var dbListings = _DbOperations.ListingsRepository().Find(l => l.Id == ListingId);
                    if (status is "Suspended" or "Declined" && string.IsNullOrWhiteSpace(dbListings.AdminMessage))
                    {
                        return false;
                    }
                    dbListings.Status = (Status)Enum.Parse(typeof(Status), status);

                    _DbOperations.ListingsRepository().Update(dbListings, dbListings.Id);

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

        public bool UpdateListingWithMessage(ListingsViewModel listingsModel)
        {
            var tranz = _DbOperations.GetDbContext();
            try
            {
                if (listingsModel != null)
                {
                    tranz.BeginTransaction();

                    var dbListings = _DbOperations.ListingsRepository().Find(l => l.Id == listingsModel.Id);
                    dbListings.AdminMessage = listingsModel.AdminMessage;
                    _DbOperations.ListingsRepository().Update(dbListings, dbListings.Id);

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
