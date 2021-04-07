using System.Threading.Tasks;
using Application.Exceptions;
using Application.Logic;
using Application.Services;
using AutoMapper;
using Database;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Logic
{
    public class UserService : GenericEntityService<User>, IUserService
    {
        private readonly IHashService _hashService;
        
        public UserService(AppDbContext context, IMapper mapper, IHashService hashService) : base(context, mapper)
        {
            _hashService = hashService;
        }
        
        public async Task<bool> IsValidUserCredentials(string email, string password)
        {
            var user = await _targetSet.FirstOrDefaultAsync(x => x.Email == email);

            if (user is null)
                throw AppError.UserNotFound;

            return _hashService.Verify(password, user.Password);
        }

        public async Task<string> GetUserRole(string email)
        {
            var user = await _targetSet.Include(x => x.Role)
                            .FirstOrDefaultAsync(x => x.Email == email);

            if (user is null)
                throw AppError.UserNotFound;

            return user.Role.Title;
        }
    }
}