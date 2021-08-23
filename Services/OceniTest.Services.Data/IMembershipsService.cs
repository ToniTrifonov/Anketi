namespace OceniTest.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IMembershipsService
    {
        Task AddMemberAsync(string userId, string subType);
    }
}
