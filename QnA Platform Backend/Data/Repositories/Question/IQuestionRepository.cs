using QnAPlatformBackend.Data.Entities;

namespace QnAPlatformBackend.Data.Repositories
{
    public interface IQuestionRepository
    {
        Task<List<Question>> GetQuestionsAsync();
        Task<Question> GetQuestionByIdAsync(int id);
        Task<Question> AddQuestionAsync(Question question);
    }
}
