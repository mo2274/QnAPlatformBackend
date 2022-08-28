using FakeItEasy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QnAPlatformBackend.Controllers;
using QnAPlatformBackend.Data.Repositories;
using QnAPlatformBackend.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace QnAPlatformBackend.Tests
{
    public class QuestionsControllerTests
    {

        [Fact]
        public async void GetReturnsAllQuestionsWithAnswers()
        {
            int count = 10;
            var fakeQuestions = A.CollectionOfDummy<QuestionModel>(count).ToList();
            var questionRepository = A.Fake<IQuestionRepository>();
            var answerRepository = A.Fake<IAnswerRepository>();
            var userRepository = A.Fake<IUserRepository>();
            var voteRepository = A.Fake<IVoteRepository>();
            A.CallTo(() => questionRepository.GetQuestionsAsync()).Returns(Task.FromResult(fakeQuestions));

            var controller = new QuestionsController(questionRepository, answerRepository,
                userRepository, voteRepository);

            var actionResult = await controller.Get();
            var result = actionResult.Result as OkObjectResult;
            var questions = result?.Value as List<QuestionModel>;

            Assert.Equal(count, questions?.Count);
            Assert.Equal(StatusCodes.Status200OK, result?.StatusCode);
        }
    }
}