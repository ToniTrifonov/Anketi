namespace OceniTest.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OceniTest.Data.Common.Models;

    public class Download : BaseDeletableModel<string>
    {
        public Download()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public string QuizId { get; set; }

        public Quiz Quiz { get; set; }
    }
}
