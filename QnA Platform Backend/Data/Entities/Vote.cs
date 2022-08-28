using QnAPlatformBackend.Utilties;
using System.Text.Json.Serialization;

namespace QnAPlatformBackend.Data.Entities
{
    public class Vote
    {
        public int Id { get; set; }
        public int AnswerId { get; set; }
        public Answer Answer { get; set; }
        public int QuestionId { get; set; }
        [JsonIgnore]
        public Question Question { get; set; }
        [JsonIgnore]
        public int UserId { get; set; }
        public User User { get; set; }
        public VoteType Value { get; set; }

    }
}
