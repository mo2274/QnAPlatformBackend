using QnAPlatformBackend.Data.Entities;
using QnAPlatformBackend.ViewModels;

namespace QnAPlatformBackend.Data.Repositories
{
    public interface IQuestionRepository
    {
        Task<List<QuestionModel>> GetQuestionsAsync();
        Task<Question> GetQuestionByIdAsync(int id);
        Task<int> AddQuestionAsync(Question question);
        Task DeleteQuestionAsync(Question question);
    }
}
