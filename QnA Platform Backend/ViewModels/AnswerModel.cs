using System.ComponentModel.DataAnnotations;

namespace QnAPlatformBackend.ViewModels
{
    public class AnswerModel
    {
        [Required]
        public string Text { get; set; }
    }
}
