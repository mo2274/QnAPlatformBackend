using Microsoft.AspNetCore.Authorization;
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

        [HttpGet]
        public async Task<ActionResult<List<QuestionModel>>> Get()
        {
            try
            {
                var questions = await questionRepository.GetQuestionsAsync();

                return Ok(questions);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Faild to retrieve questions");
            }
        }

        [HttpGet("{questionId:int}")]
        public async Task<ActionResult<Question>> Get(int questionId)
        {
            try
            {
                var question = await questionRepository.GetQuestionByIdAsync(questionId);

                if (question == null)
                    return NotFound("Question not found");

                return Ok(question.Text);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Faild to retrieve question");
            }
        }

        [HttpDelete("{questionId:int}")]
        public async Task<ActionResult<Question>> Delete(int questionId)
        {
            try
            {
                var question = await questionRepository.GetQuestionByIdAsync(questionId);

                if (question == null)
                    return BadRequest("Question not found");

                await questionRepository.DeleteQuestionAsync(question);

                return Ok("Question deleted successfully");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Faild to delete question");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(QuestionModel model)
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier)
                    .FirstOrDefault().Value);
                var user = await userRepository.GetUserByIdAsync(userId);

                var question = new Question()
                {
                    Text = model.Text,
                    User = user
                };

                var questionId = await questionRepository.AddQuestionAsync(question);

                return CreatedAtAction(nameof(Get), new { questionId = question.Id }, "Question created successfully");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Faild to Add questions");
            }

        }

        [HttpPost("{questionId:int}/answers")]
        public async Task<ActionResult<string>> Post(int questionId, AnswerModel model)
        {
            try
            {
                var question = await questionRepository.GetQuestionByIdAsync(questionId);

                if (question == null)
                    return BadRequest();

                var userId = Convert.ToInt32(User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier)
                    .FirstOrDefault().Value);
                var user = await userRepository.GetUserByIdAsync(userId);

                var answer = new Answer()
                {
                    Text = model.Text,
                    Question = question,
                    User = user
                };

                answer = await answerRepository.AddAnswer(answer);

                return CreatedAtAction(nameof(Get), new { answerId = answer.Id }, "Answer created successfully");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Faild to Add Answer");
            }
        }

        [HttpDelete("{questionId:int}/answers/{answerId:int}")]
        public async Task<IActionResult> Delete(int questionId, int answerId)
        {
            try
            {
                var question = await questionRepository.GetQuestionByIdAsync(questionId);

                if (question == null)
                    return BadRequest("Question not found");

                var answer = await answerRepository.GetAnswerByIdAsync(answerId);

                if (answer == null)
                    return BadRequest("answer not found");

                await answerRepository.DeleteAnswerAsync(answer);

                return Ok("Answer is deleted successfully");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Faild to delete Answer");
            }
        }

        [HttpPut("{questionId:int}/answers/{answerId:int}/votes")]
        public async Task<IActionResult> Put(int questionId, int answerId, VoteModel vote)
        {
            try
            {
                var question = await questionRepository.GetQuestionByIdAsync(questionId);

                if (question == null)
                    return BadRequest("Question not found");

                var answer = await answerRepository.GetAnswerByIdAsync(answerId);

                if (answer == null)
                    return BadRequest("answer not found");

                var userId = Convert.ToInt32(User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier)
                   .FirstOrDefault().Value);
                var user = await userRepository.GetUserByIdAsync(userId);

                await voteRepository.UpdateVoteAsync(question, answer, user, vote.Value);

                return Ok("Vote updated successfully");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Faild to update vote");
            }
        }
    }
}
