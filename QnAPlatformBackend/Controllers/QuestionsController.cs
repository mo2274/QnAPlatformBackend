// QnAPlatformBackend/Controllers/QuestionsController.cs

using Microsoft.AspNetCore.Mvc;
using QnAPlatformBackend.Data.Entities;
using QnAPlatformBackend.Data.Repositories;
using QnAPlatformBackend.ViewModels;
using System.Security.Claims;

namespace QnAPlatformBackend.Controllers
{
    [Route("/questions")]
    [ApiController]
    [Authorize]
    public class QuestionsController : ControllerBase
    {
        private readonly IQuestionRepository questionRepository;
        private readonly IAnswerRepository answerRepository;
        private readonly IUserRepository userRepository;
        private readonly IVoteRepository voteRepository;

        public QuestionsController(
            IQuestionRepository questionRepository,
            IAnswerRepository answerRepository,
            IUserRepository userRepository,
            IVoteRepository voteRepository)
        {
            this.questionRepository = questionRepository;
            this.answerRepository = answerRepository;
            this.userRepository = userRepository;
            this.voteRepository = voteRepository;
        }

        // Existing GET endpoints...

        [HttpPut("{questionId:int}")]
        public async Task<ActionResult<Question>> Update(int questionId, [FromBody] string newText)
        {
            var question = await questionRepository.GetQuestionByIdAsync(questionId);

            if (question == null)
            {
                return NotFound();
            }

            question.Text = newText;

            await questionRepository.UpdateQuestionAsync(question);

            return Ok(question);
        }
    }
}