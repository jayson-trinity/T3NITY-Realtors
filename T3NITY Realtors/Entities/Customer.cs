namespace T3NITY_Realtors.Entities
{
    public class Customer
    {
        public int Id { get; set; } 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int PhoneNumber { get; set; }
        public int UsersId { get; set; }
        public Users Users { get; set; }
    }
}
