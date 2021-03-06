using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using JobManager.Models;
using Newtonsoft.Json;

namespace JobManager.Services
{
    class JobDataStoreLocalJson : IJobDataStore<Job>
    {
        public static string FilePath
    {
        get
        {
            var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            return Path.Combine(basePath, "Jobs.json");
        }
    }
 
public async Task AddJob(Job job)
        {
            var jobs = ReadFile();
            jobs.Add(job);
            WriteFile(jobs); 
        }

 public async Task DeleteJob(Job job)
        {
            var jobs = ReadFile();
            var remove = jobs.Find(p => p.Id == job.Id); 
            jobs.Remove(remove); 
        }

       

        public async Task UpdateJob(Job job)
        {
            var jobs = ReadFile();
            jobs[jobs.FindIndex(p => p.Id == job.Id)] = job;
            WriteFile(jobs); 
        }
    
        private List<Job> ReadFile()
        {
            File.Exists(FilePath);
            try
            {
                var jsonString = File.ReadAllText(FilePath);
                var jobs = JsonConvert.DeserializeObject<List<Job>>(jsonString);
                return jobs; 
            }

            catch (Exception e)
            {
                var defaultJobs = GetDefaultJobs();
                WriteFile(defaultJobs);
                return defaultJobs; 
            }
        }

        private List<Job> GetDefaultJobs()
        {
            var jobs = new List<Job>()
            {
             new Job { Id = 1, Name = "Job A local Json File", Description = "This is job a." },
             new Job { Id = 2, Name = "Job B local Json File", Description = "This is job b." },
             new Job { Id = 3, Name = "Job C local Json File", Description = "This is job c." },
             new Job { Id = 4, Name = "Job D local Json File", Description = "This is job d." }
          //  throw new NotImplementedException();
        };
            return jobs; 
        }
        private void WriteFile(List<Job> jobs)
        {
            var jsonString = JsonConvert.SerializeObject(jobs);
            File.WriteAllText(FilePath, jsonString);
        }

        public async Task<Job> GetJob(int jobId)
        {
            var jobs = ReadFile();
            var job = jobs.Find(p => p.Id == jobId);
            return job;
        }

        public async Task<IEnumerable<Job>> GetJobs()
        {
            var jobs = ReadFile();
            return jobs;
        }

    }
}
