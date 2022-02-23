using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobManager.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JobManager.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WelcomePage : ContentPage
    {
        public WelcomePage()
        {
            InitializeComponent();
        }

        private async void OnAccessAPIClicked(object sender, EventArgs e)
        {
            var service = DependencyService.Get<IWebClientService>();
            var content = await service.GetString("https://www.google.com"); 

        }
    }
}