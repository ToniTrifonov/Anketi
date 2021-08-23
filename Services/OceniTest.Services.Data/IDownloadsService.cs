namespace OceniTest.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IDownloadsService
    {
        Task SubmitDownload(string userId, string quizId);

        int GetCount(string userId);
    }
}
