using Microsoft.EntityFrameworkCore;
using QnAPlatformBackend.Data.Entities;
using QnAPlatformBackend.Utilties;

namespace QnAPlatformBackend.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly QnADbContext context;

        public UserRepository(QnADbContext context)
        {
            this.context = context;
        }
        public async Task<User> GetByUsernameAndPasswordAsync(string username, string password)
        {
            var user = await context.Users
                .Where(u => u.UserName == username && u.Password == password.Sha256())
                .FirstOrDefaultAsync();
            return user;
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            var user = await context.Users.FindAsync(userId);
            return user;
        }
    }
}
