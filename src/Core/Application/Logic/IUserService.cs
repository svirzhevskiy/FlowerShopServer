using System.Threading.Tasks;

namespace Application.Logic
{
    public interface IUserService
    {
        public Task<bool> IsValidUserCredentials(string email, string password);
        public Task<string> GetUserRole(string email);
    }
}