using System.ComponentModel.DataAnnotations;

namespace QnAPlatformBackend.Data.Entities
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        public List<Answer> Answers { get; set; }
        public List<Question> Questions { get; set; }
        public List<Vote> Votes { get; set; }
    }
}
