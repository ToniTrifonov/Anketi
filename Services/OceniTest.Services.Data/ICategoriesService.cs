namespace OceniTest.Services.Data
{
    using System.Collections.Generic;

    using OceniTest.Web.ViewModels.Categories;

    public interface ICategoriesService
    {
        IEnumerable<KeyValuePair<string, string>> GetAll();
    }
}
