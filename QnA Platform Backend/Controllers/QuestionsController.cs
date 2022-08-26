using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QnAPlatformBackend.Data.Entities;
using QnAPlatformBackend.Data.Repositories;
using QnAPlatformBackend.Models;

namespace QnAPlatformBackend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class QuestionsController : ControllerBase
    {
        private readonly IQuestionRepository questionRepository;
        private readonly IAnswerRepository answerRepository;

        public QuestionsController(IQuestionRepository questionRepository, IAnswerRepository answerRepository)
        {
            this.questionRepository = questionRepository;
            this.answerRepository = answerRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Question>>> Get()
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
                    return NotFound();

                return Ok(question);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Faild to retrieve question");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Question>> Post(QuestionModel model)
        {
            try
            {
                //TODO Get the user data

                var question = new Question() { Text = model.Text };
                question = await questionRepository.AddQuestionAsync(question);

                return CreatedAtAction(nameof(Get), new { questionId = question.Id }, question);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Faild to Add questions");
            }

        }

        [HttpPost("{questionId:int}/answers")]
        public async Task<ActionResult<Answer>> Post(int questionId, AnswerModel model)
        {
            try
            {
                var question = await questionRepository.GetQuestionByIdAsync(questionId);

                if (question == null)
                    return BadRequest();

                //TODO GET THE USER DATA

                var answer = new Answer()
                {
                    Text = model.Text,
                    Question = question
                };

                answer = await answerRepository.AddAnswer(answer);

                return Ok(answer);
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
                    return BadRequest();

                var answer = await answerRepository.GetAnswerByIdAsync(answerId);

                if (answer == null)
                    return BadRequest();

                // GET THE USER DATA

                await answerRepository.DeleteAnswerAsync(answer);

                return Ok();
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
                    return BadRequest();

                var answer = await answerRepository.GetAnswerByIdAsync(answerId);

                if (answer == null)
                    return BadRequest();

                //TODO GET THE USER DATA
                //TODO ADD VOTE INTERFACE WITH A METHOD UPDATE VOTE


                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Faild to update vote");
            }
        }
    }
}
