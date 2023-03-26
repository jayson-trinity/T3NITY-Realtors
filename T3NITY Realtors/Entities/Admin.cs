namespace T3NITY_Realtors.Entities
{
    public class Admin
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int UsersId { get; set; }
        public Users Users { get; set; }
    }
}
