using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using JobManager.Models;
using MvvmHelpers;
using MvvmHelpers.Commands;

namespace JobManager.ViewModels
{
    class JobListViewModel : JobManagerBase
    {
        public ObservableRangeCollection<Job> Jobs { get; set; }
        public AsyncCommand RefreshCommand { get; }

        public AsyncCommand<Job> SelectCommand { get; }

        private Job selectedJob;

        public Job SelectedJob
        { 
        get => selectedJob; 
        set => SetProperty(ref selectedJob, value); 
        }

        public JobListViewModel()
        {
            Title = "Jobs";

            Jobs = new ObservableRangeCollection<Job>(); 
            RefreshCommand = new AsyncCommand(Refresh);
            SelectCommand = new AsyncCommand<Job>(Selected);
        }

        private async Task Selected(Job job)
        {
            //not implemented 
        }

        public async Task Refresh()
        {
            IsBusy = true;
            Jobs.Clear();
            LoadJobs();

            IsBusy = false; 
        }

        public async void LoadJobs()
        {
            IEnumerable<Job> jobs = await JobDataStore.GetJobs();
            Jobs.AddRange(jobs); 
        }
    }
}
