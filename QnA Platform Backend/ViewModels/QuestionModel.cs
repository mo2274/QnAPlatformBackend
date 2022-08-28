using System.ComponentModel.DataAnnotations;

namespace QnAPlatformBackend.ViewModels
{
    public class QuestionModel
    {
        [Required]
        public string Text { get; set; }
        public List<AnswerModel> Answers { get; set; }
    }
}
