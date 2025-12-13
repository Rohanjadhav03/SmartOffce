using BackendApi.Models;

namespace BackendApi.Repositories
{
    public interface IAuthRepository
    {
        public Task<User> ValidateUserAsync(string username, string password);
    }
}
