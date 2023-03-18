namespace T3NITY_Realtors.Extentions
{
    public static class IFormFileExtension
    {
        public static byte[] GetBytes(this IFormFile formFile)
        {
            var memoryStream = new MemoryStream();
            formFile.CopyTo(memoryStream);
            return memoryStream.ToArray();
        }

        public static string GetBase64String(this IFormFile formFile)
        {
            using var ms = new MemoryStream();
            formFile.CopyTo(ms);
            string stringFile = Convert.ToBase64String(ms.ToArray());
            return stringFile;
        }
    }
}
