using System.Security.Cryptography;
using System.Text;

namespace T3NITY_Realtors
{
    public static class UtilData
    {
        public const string Customer = "Customer";
        public const string Landlord = "Landlord";
        public const string UserId = "UserId";
        public const string UserName = "UserName";
        public const string FirstName = "FirstName";
        public const string Role = "Role";

        public static string GetHash(string text)
        {
            // SHA512 is disposable by inheritance.  
            using var sha256 = SHA256.Create();
            // Send a sample text to hash.  
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(text));
            // Get the hashed string.  
            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }
    }
}
