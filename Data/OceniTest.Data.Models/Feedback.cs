﻿namespace OceniTest.Data.Models
{
    using System;

    using OceniTest.Data.Common.Models;

    public class Feedback : BaseDeletableModel<string>
    {
        public Feedback()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Comments { get; set; }

        public string UserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public string QuizId { get; set; }

        public Quiz Quiz { get; set; }
    }
}
