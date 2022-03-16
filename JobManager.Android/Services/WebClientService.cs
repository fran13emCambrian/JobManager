using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JobManager.Services;
using System.Threading.Tasks;
using System.Net.Http;
using Xamarin.Forms;
using JobManager;

[assembly: Dependency(typeof(WebClientService))]
namespace JobManager
{
    public class WebClientService : IWebClientService
    {
        public async Task<string> GetAsync(string uri)
        {
            try
            {
                var client = new HttpClient();
                var response = await client.GetAsync(uri);

                return response.IsSuccessStatusCode ? await response.Content.ReadAsStringAsync() : null;
            }
            catch
            {
                return null; 
            }

        }

        public async Task<string> PostAsync(string uri, string body, string type)
        {
            try
            {
                HttpClient client;
                client = new HttpClient();

                var content = new StringContent(body, Encoding.UTF8, type);

                HttpResponseMessage response = await client.PostAsync(uri, content); 
                return response.IsSuccessStatusCode ? await response.Content.ReadAsStringAsync() : null;
            }
            catch
            {
                return null;
            }
        }


        public async Task<string> PutAsync(string uri, string body, string type)
        {
            try
            {
                HttpClient client;
                client = new HttpClient();

                var content = new StringContent(body, Encoding.UTF8, type);

                HttpResponseMessage response = await client.PutAsync(uri, content);
                return response.IsSuccessStatusCode ? await response.Content.ReadAsStringAsync() : null;
            }
            catch
            {
                return null;
            }
        }
    }
}