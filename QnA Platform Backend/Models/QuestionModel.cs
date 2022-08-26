using System.ComponentModel.DataAnnotations;

namespace QnAPlatformBackend.Models
{
    public class QuestionModel
    {
        [Required]
        public string Text { get; set; }
    }
}
