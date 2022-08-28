using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace QnAPlatformBackend.Data.Entities
{
    public class Answer
    {
        public int Id { get; set; }
        [Required]
        public string Text { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int QuestionId { get; set; }
        [JsonIgnore]
        public Question Question { get; set; }
        public List<Vote> Votes { get; set; }
    }
}
