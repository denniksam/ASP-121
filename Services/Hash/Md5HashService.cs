namespace ASP121.Services.Hash
{
    public class Md5HashService : IHashService
    {
        public string HashString(string source)
        {
            using var md5 = System.Security.Cryptography.MD5.Create();
            return Convert.ToHexString(
                md5.ComputeHash(
                    System.Text.Encoding.UTF8.GetBytes(source)
            ));
        }
    }
}
