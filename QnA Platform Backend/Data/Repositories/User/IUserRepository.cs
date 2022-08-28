using QnAPlatformBackend.Data.Entities;

namespace QnAPlatformBackend.Data.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetByUsernameAndPasswordAsync(string username, string password);
        Task<User> GetUserByIdAsync(int userId);
    }
}
