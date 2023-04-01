namespace T3NITY_Realtors.Models
{
    public class LandLordDashModel
    {
        public int Posted { get; set; }
        public int Approved { get; set; }
        public int Pending { get; set; }
        public IEnumerable<ListingsViewModel> Listings { get; set; }
    }
}
