﻿using System.ComponentModel.DataAnnotations;

namespace QnAPlatformBackend.Data.Entities
{
    public class Question
    {
        public int Id { get; set; }
        [Required]
        public string Text { get; set; } = string.Empty;
        public List<Answer> Answers { get; set; }
        public List<Vote> Votes { get; set; }
    }
}