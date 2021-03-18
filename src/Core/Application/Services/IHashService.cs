namespace Application.Services
{
    public interface IHashService
    {
        public bool Verify(string text, string hash);
        public string GetHash(string text);
    }
}
