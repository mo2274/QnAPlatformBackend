using QnAPlatformBackend.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QnAPlatformBackend.Data.Repositories
{
    public interface IQuestionRepository
    {
        Task<int> AddQuestionAsync(Question question);
        Task DeleteQuestionAsync(Question question);
        Task<Question> GetQuestionByIdAsync(int id);
        Task<List<Question>> GetQuestionsAsync();
        Task UpdateQuestionAsync(Question question);
    }
}
