namespace ASP121.Services.Hash
{
    public class Sha1HashService : IHashService
    {
        public string HashString(string source)
        {
            using var hasher = System.Security.Cryptography.SHA1.Create();
            return Convert.ToHexString(
                hasher.ComputeHash(
                    System.Text.Encoding.UTF8.GetBytes(source)
            ));
        }
    }
}
