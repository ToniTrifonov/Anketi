namespace OceniTest.Data.Models
{
    using System.Collections.Generic;

    using OceniTest.Data.Common.Models;

    public class Category : BaseDeletableModel<string>
    {
        public Category()
        {
            this.CategoryQuizzes = new HashSet<Quiz>();
        }

        public string Name { get; set; }

        public ICollection<Quiz> CategoryQuizzes { get; set; }
    }
}
