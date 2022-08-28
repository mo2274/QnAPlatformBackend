using QnAPlatformBackend.Data.Entities;
using QnAPlatformBackend.Utilties;

namespace QnAPlatformBackend.Data.Repositories
{
    public interface IVoteRepository
    {
        public Task UpdateVoteAsync(Question question, Answer answer, User user, VoteType vote);
    }
}
