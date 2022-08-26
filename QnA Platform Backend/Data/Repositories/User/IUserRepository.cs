using QnAPlatformBackend.Data.Entities;

namespace QnAPlatformBackend.Data.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetByUsernameAndPassword(string username, string password);
    }
}
