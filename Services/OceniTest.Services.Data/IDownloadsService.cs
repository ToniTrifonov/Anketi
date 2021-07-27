using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OceniTest.Services.Data
{
    public interface IDownloadsService
    {
        Task SubmitDownload(string userId, string quizId);

        int GetCount(string userId);
    }
}
