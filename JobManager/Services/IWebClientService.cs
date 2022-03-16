using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JobManager.Services
{
    public interface IWebClientService
    {
        Task<string> GetAsync(string uri);
        Task<string> PostAsync(string uri, string body, string type); 
    }
}
