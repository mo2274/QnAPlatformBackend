using QnAPlatformBackend.Data.Entities;

namespace QnAPlatformBackend.Data.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        public Task<Question> AddQuestionAsync(Question question)
        {
            throw new NotImplementedException();
        }

        public Task<Question> GetQuestionByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Question>> GetQuestionsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
