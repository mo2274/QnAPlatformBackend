using Microsoft.EntityFrameworkCore;
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
        public async Task<int> AddQuestionAsync(Question question)
        {
            context.Questions.Add(question);
            await context.SaveChangesAsync();
            return question.Id;
        }

        public async Task DeleteQuestionAsync(Question question)
        {
            context.Questions.Remove(question);
            await context.SaveChangesAsync();
        }

        public async Task<Question> GetQuestionByIdAsync(int id)
        {
            return await context.Questions.FindAsync(id);
        }

        public async Task<List<QuestionModel>> GetQuestionsAsync()
        {
            var query = context.Questions
                            .Include(q => q.Answers)
                            .Select(q => new QuestionModel()
                            {
                                Text = q.Text,
                                Answers = q.Answers.Select(a => new AnswerModel() { Text = a.Text }).ToList()
                            });
            return await query.ToListAsync();
        }

        // TODO LATER
        public async Task<List<QuestionModel>> GetQuestionsAsyncOLD()
        {
            return await Task.Run(() =>
            {
                var query = context.Questions
                        .Include(q => q.Answers)
                        .ThenInclude(a => a.Votes)
                        .AsEnumerable()
                        .OrderByDescending(
                                  q => q.Answers
                                  .GroupBy(a => a.Id)
                                  .Select(g =>
                                            new { count = g.Max(a => a.Votes.Where(v => v.Value == VoteType.Up).Count()) })
                                  .Max().count

                                  *

                                  q.Answers.Count()

                                  -

                                  q.Answers
                                  .GroupBy(a => a.Id)
                                  .Select(g =>
                                            new { VoteScore = g.Max(a => a.Votes.Sum(v => (int)v.Value)) }
                                         )
                                  .Select(s => s.VoteScore < 0).Count()

                                 )
                        .Select(q =>
                            new QuestionModel()
                            {
                                Text = q.Text,
                                Answers = q.Answers
                                           .Select(a => new AnswerModel() { Text = a.Text })
                                           .ToList()
                            });

                return query.ToList();
            });
        }
    }
}
