using System;
using System.Collections.Generic;
using System.Text;
using MvvmHelpers;
using JobManager.Services;
using JobManager.Models;
using Xamarin.Forms; 

namespace JobManager.ViewModels
{
public class JobManagerBase : BaseViewModel 
    {
        public IJobDataStore<Job> JobDataStore => DependencyService.Get<IJobDataStore<Job>>();
    }
}
