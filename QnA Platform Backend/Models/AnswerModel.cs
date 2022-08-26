using System.ComponentModel.DataAnnotations;

namespace QnAPlatformBackend.Models
{
    public class AnswerModel
    {
        [Required]
        public string Text { get; set; }
    }
}
