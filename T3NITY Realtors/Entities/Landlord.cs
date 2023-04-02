﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace T3NITY_Realtors.Entities
{
    public class Landlord : BaseEntity
    {
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int UsersId { get; set; }
        public Users Users { get; set; }
    }
}
