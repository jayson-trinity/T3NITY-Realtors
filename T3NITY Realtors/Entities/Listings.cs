using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace T3NITY_Realtors.Entities
{
    public class Listings
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public string ContactInfo { get; set; }
        public decimal Price { get; set; }
        public string PerWhat { get; set; }
        public string Status { get; set; }
        public string AdminMessage { get; set; }
        public int UsersId { get; set; }
        public Users Users { get; set; }
    }

}
