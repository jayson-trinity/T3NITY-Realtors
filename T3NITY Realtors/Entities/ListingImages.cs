using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace T3NITY_Realtors.Entities
{
    public class ListingImages : BaseEntity
    {
        
        public byte[] FileByte { get; set; }
        public string Extension { get; set; }
        public string FileName { get; set; }
        public bool IsDefault { get; set; }
        public int ListingsId { get; set; }
        public Listings Listings { get; set; }
    }
}
