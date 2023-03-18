using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace T3NITY_Realtors.Entities
{
    public class ListingImages
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public byte[] FileByte { get; set; }
        public string Extension { get; set; }
        public string FileName { get; set; }
        public bool IsDefault { get; set; }
        public int ListingsId { get; set; }
        public Listings Listings { get; set; }
    }
}
