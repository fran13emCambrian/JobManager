using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JobManager.Services
{
    public interface IWebClientService
    {
        Task<string> GetString(string uri);
    }
}
