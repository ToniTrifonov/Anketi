namespace OceniTest.Data.Models
{
    using System;
    using System.Collections.Generic;

    using OceniTest.Data.Common.Models;

    public class Category : BaseDeletableModel<string>
    {
        public Category()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CategoryQuizzes = new HashSet<Quiz>();
        }

        public string Name { get; set; }

        public virtual ICollection<Quiz> CategoryQuizzes { get; set; }
    }
}
