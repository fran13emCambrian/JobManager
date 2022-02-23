using System;
using System.Collections.Generic;
using System.Text;
using JobManager.Models;
using JobManager.Services;  
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models; 
using System.IO;
using Newtonsoft.Json;

namespace JobManager.Services
{
    class JobDataStoreBlobStorageJson : IJobDataStore<Job>
    {

        private readonly BlobServiceClient service = new BlobServiceClient(ConnectionString);
        private static string ConnectionString => "Connection String Goes Here";
        private static string Container => "data"; 
        private static string Filename => "Jobs.json"; 

      

        public async Task WriteFile(List<Job> jobs)
        {
            var jsonString = JsonConvert.SerializeObject(jobs);
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Flush();
            stream.Position = 0;
            BlobClient blob = service.GetBlobContainerClient(Container).GetBlobClient(Filename);
            await blob.UploadAsync(stream); 

        }

        public async Task<List<Job>> ReadFile()
        {
            BlobClient blob = service.GetBlobContainerClient(Container).GetBlobClient(Filename);

            if (blob.Exists())
            {
                var stream = new MemoryStream();
                await blob.DownloadToAsync(stream);

                stream.Position = 0;
                var jsonString = new StreamReader(stream).ReadToEnd();
                var jobs = JsonConvert.DeserializeObject<List<Job>>(jsonString);
                return jobs;
            }
            else
            {
                var defaultJobs = GetDefaultJobs();

                await WriteFile(defaultJobs);

                return defaultJobs; 
            }
        }


        private List<Job> GetDefaultJobs()
        {
            var jobs = new List<Job>()
            {
             new Job { Id = 1, Name = "Job A local Azure Blob File", Description = "This is job a." },
             new Job { Id = 2, Name = "Job B local Azure Blob File", Description = "This is job b." },
             new Job { Id = 3, Name = "Job C local Azure Blob File", Description = "This is job c." },
             new Job { Id = 4, Name = "Job D local Azure Blob File", Description = "This is job d." }
          //  throw new NotImplementedException();
        };
            return jobs;
        }

        public async Task AddJob(Job job)
        {
            var jobs = await ReadFile();
            jobs.Add(job);
          await  WriteFile(jobs);
        }

        public async Task DeleteJob(Job job)
        {
            throw new NotImplementedException();
        }

        public async Task<Job> GetJob(int jobId)
        {
            var jobs = await ReadFile();
            var job = jobs.Find(p => p.Id == jobId);
            return job; 
        }

        public async Task<IEnumerable<Job>> GetJobs()
        {
            var jobs = await ReadFile();
            return jobs; 
        }

        public async Task UpdateJob(Job job)
        {
            var jobs = await ReadFile();
            jobs[jobs.FindIndex(p => p.Id == job.Id)] = job;
           await WriteFile(jobs);
        }
    }
}
