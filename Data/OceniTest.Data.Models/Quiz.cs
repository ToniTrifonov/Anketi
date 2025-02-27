﻿namespace OceniTest.Data.Models
{
    using System;
    using System.Collections.Generic;

    using OceniTest.Data.Common.Models;

    public class Quiz : BaseDeletableModel<string>
    {
        public Quiz()
        {
            this.Id = Guid.NewGuid().ToString();
            this.QuizQuestions = new HashSet<Question>();
            this.QuizFeedbacks = new HashSet<Feedback>();
            this.QuizDownloads = new HashSet<Download>();
        }

        public string Name { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public bool IsPrivate { get; set; }

        public string CategoryId { get; set; }

        public Category Category { get; set; }

        public string UserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public virtual ICollection<Question> QuizQuestions { get; set; }

        public virtual ICollection<Feedback> QuizFeedbacks { get; set; }

        public virtual ICollection<Download> QuizDownloads { get; set; }
    }
}
