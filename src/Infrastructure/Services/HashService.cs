using Application.Services;

namespace Services
{
    public class HashService : IHashService
    {
        public string GetHash(string text)
        {
            return BCrypt.Net.BCrypt.HashPassword(text);
        }

        public bool Verify(string text, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(text, hash);
        }
    }
}
