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

        public async Task<Question> UpdateQuestionAsync(int id, string newText)
        {
            var question = await context.Questions.FindAsync(id);

            if (question == null)
            {
                return null;
            }

            question.Text = newText;

            await context.SaveChangesAsync();

            return question;
        }
    }
}