using FakeItEasy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QnAPlatformBackend.Controllers;
using QnAPlatformBackend.Data.Entities;
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

        [Fact]
        public async void GetQuestionByIdReturnsTheSelectedQuestion()
        {
            int questionId = 1;
            string questionText = "nnnnnnnn";

            var question = new Question()
            {
                Id = questionId,
                Text = questionText
            };
            var questionRepository = A.Fake<IQuestionRepository>();
            var answerRepository = A.Fake<IAnswerRepository>();
            var userRepository = A.Fake<IUserRepository>();
            var voteRepository = A.Fake<IVoteRepository>();
            A.CallTo(() => questionRepository.GetQuestionByIdAsync(questionId)).Returns(Task.FromResult(question));

            var controller = new QuestionsController(questionRepository, answerRepository,
                userRepository, voteRepository);

            var actionResult = await controller.Get(questionId);
            var result = actionResult.Result as OkObjectResult;
            var resultQuestion = result?.Value as string;

            Assert.Equal(questionText, resultQuestion);
            Assert.Equal(StatusCodes.Status200OK, result?.StatusCode);
        }

        [Fact]
        public async void DeleteQuestionByIdReturnsOk()
        {
            int questionId = 1;
            string questionText = "nnnnnnnn";

            var question = new Question()
            {
                Id = questionId,
                Text = questionText
            };
            var questionRepository = A.Fake<IQuestionRepository>();
            var answerRepository = A.Fake<IAnswerRepository>();
            var userRepository = A.Fake<IUserRepository>();
            var voteRepository = A.Fake<IVoteRepository>();
            A.CallTo(() => questionRepository.GetQuestionByIdAsync(questionId)).Returns(Task.FromResult(question));
            A.CallTo(() => questionRepository.DeleteQuestionAsync(question)).Returns(Task.CompletedTask);

            var controller = new QuestionsController(questionRepository, answerRepository,
                userRepository, voteRepository);

            var actionResult = await controller.Delete(questionId);
            var result = actionResult.Result as OkObjectResult;

            Assert.Equal(StatusCodes.Status200OK, result?.StatusCode);
        }
    }
}