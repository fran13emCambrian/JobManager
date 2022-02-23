using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms; 

namespace JobManager.ViewModels
{
    [QueryProperty(nameof(JobId), nameof(JobId))]
    public class JobDetailViewModel : JobManagerBase
    {
        private int jobId;
        public int JobId
        {
            get
            {
                return jobId; 
            }
            set
            {
                jobId = value;
                LoadJob(value); 

            }
        }

        private string name; 
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value); 
        }

        private string description;
        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        public async void LoadJob(int jobId)
        {
            var job = await JobDataStore.GetJob(jobId);
            Name = job.Name;
            Description = job.Description; 
        }

    }
}
