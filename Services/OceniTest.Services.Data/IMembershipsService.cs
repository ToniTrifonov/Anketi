using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OceniTest.Services.Data
{
    public interface IMembershipsService
    {
        Task AddMember(string userId);
    }
}
