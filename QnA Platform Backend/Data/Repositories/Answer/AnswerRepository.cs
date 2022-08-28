using QnAPlatformBackend.Data.Entities;

namespace QnAPlatformBackend.Data.Repositories
{
    public class AnswerRepository : IAnswerRepository
    {
        private readonly QnADbContext context;

        public AnswerRepository(QnADbContext context)
        {
            this.context = context;
        }

        public async Task<Answer> AddAnswer(Answer answer)
        {
            context.Answers.Add(answer);
            await context.SaveChangesAsync();
            return answer;
        }

        public async Task DeleteAnswerAsync(Answer answer)
        {
            context.Answers.Remove(answer);
            await context.SaveChangesAsync();
        }

        public async Task<Answer> GetAnswerByIdAsync(int id)
        {
            return await context.Answers.FindAsync(id);
        }
    }
}
