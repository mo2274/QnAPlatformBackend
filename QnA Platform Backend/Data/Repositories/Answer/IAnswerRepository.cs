using QnAPlatformBackend.Data.Entities;

namespace QnAPlatformBackend.Data.Repositories
{
    public interface IAnswerRepository
    {
        Task<Answer> AddAnswer(Answer answer);
        Task<Answer> GetAnswerByIdAsync(int id);
        Task DeleteAnswerAsync(Answer answer);
    }
}
