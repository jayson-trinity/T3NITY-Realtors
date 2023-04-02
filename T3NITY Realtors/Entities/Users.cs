using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace T3NITY_Realtors.Entities
{
    public class Users:BaseEntity
    {
       
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        
    }
}
