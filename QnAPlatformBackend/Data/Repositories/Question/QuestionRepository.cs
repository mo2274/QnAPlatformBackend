// QnAPlatformBackend/Data/Repositories/Question/QuestionRepository.cs

using QnAPlatformBackend.Data.Entities;
using QnAPlatformBackend.Utilties;
using QnAPlatformBackend.ViewModels;

namespace QnAPlatformBackend.Data.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly QnADbContext context;

        public QuestionRepository(QnADbContext context)
        {
            this.context = context;
        }

        // Existing methods...

        public async Task UpdateQuestionAsync(Question question)
        {
            context.Questions.Update(question);
            await context.SaveChangesAsync();
        }
    }
}