using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace QnAPlatformBackend.Data.Entities
{
    public class Question
    {
        public int Id { get; set; }
        [Required]
        public string Text { get; set; } = string.Empty;
        public int UserId { get; set; }
        public User User { get; set; }
        public List<Answer> Answers { get; set; }
        [JsonIgnore]
        public List<Vote> Votes { get; set; }
    }
}
