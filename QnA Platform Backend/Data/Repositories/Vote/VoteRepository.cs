using Microsoft.EntityFrameworkCore;
using QnAPlatformBackend.Data.Entities;
using QnAPlatformBackend.Utilties;

namespace QnAPlatformBackend.Data.Repositories
{
    public class VoteRepository : IVoteRepository
    {
        private readonly QnADbContext context;

        public VoteRepository(QnADbContext context)
        {
            this.context = context;
        }

        public async Task UpdateVoteAsync(Question question, Answer answer, User user, VoteType vote)
        {
            if (vote == VoteType.UnVote)
                await DeleteVoteAsync(question, answer, user);
            else
                await AddVoteAsync(question, answer, user, vote);
        }

        private async Task AddVoteAsync(Question question, Answer answer, User user, VoteType vote)
        {
            var existingVote = await context.Votes
                .Where(v => v.QuestionId == question.Id && v.AnswerId == answer.Id && v.UserId == user.Id)
                .SingleOrDefaultAsync();

            if (existingVote == null)
                await AddNewVoteAsync(question, answer, user, vote);
            else
                await UpdateExistingVote(vote, existingVote);

        }

        private async Task UpdateExistingVote(VoteType vote, Vote existingVote)
        {
            existingVote.Value = vote;
            context.Votes.Update(existingVote);
            await context.SaveChangesAsync();
        }

        private async Task AddNewVoteAsync(Question question, Answer answer, User user, VoteType vote)
        {
            var newVote = new Vote()
            {
                Value = vote,
                Question = question,
                Answer = answer,
                User = user
            };

            context.Votes.Add(newVote);
            await context.SaveChangesAsync();
        }

        private async Task DeleteVoteAsync(Question question, Answer answer, User user)
        {
            var vote = await context.Votes
                .Where(v => v.QuestionId == question.Id && v.AnswerId == answer.Id && v.UserId == user.Id)
                .SingleOrDefaultAsync();

            if (vote == null)
                return;

            context.Remove(vote);
            await context.SaveChangesAsync();
        }
    }
}
