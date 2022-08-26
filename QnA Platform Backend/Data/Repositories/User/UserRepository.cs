using QnAPlatformBackend.Data.Entities;

namespace QnAPlatformBackend.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        public Task<User> GetByUsernameAndPassword(string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}
