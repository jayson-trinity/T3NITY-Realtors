namespace T3NITY_Realtors.Entities
{
    public class Payments : BaseEntity
    {
        public string InvoiceId { get; set; }
        public string InvoiceGiud { get; set; }
        public string InvoiceUrl { get; set; }
        public string Status { get; set; }
        public decimal Amount { get; set; }
        public int? ListingsId { get; set; }
        public Listings Listings { get; set; }
        public int? UsersId { get; set; }
        public Users Users { get; set; }
    }
}
