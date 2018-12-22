using AircallEmployee.PageModels.Base;
using AircallEmployee.Pages;
using AircallEmployee.Servives.Interfaces;
using AircallEmployee.Utils;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace AircallEmployee
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            AdaptColorsToHexString();
        }

        private Task InitNavigation()
        {
            var navigationService = PageModelLocator.Instance.Resolve<INavigationService>();
            return navigationService.InitializeAsync();
        }

        protected override void OnStart()
        {
            InitNavigation();
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
        private void AdaptColorsToHexString()
        {
            for (var i = 0; i < this.Resources.Count; i++)
            {
                var key = this.Resources.Keys.ElementAt(i);
                var resource = this.Resources[key];

                if (resource is Color)
                {
                    var color = (Color)resource;
                    this.Resources.Add(key + "HexString", color.ToHexString());
                }
            }
        }
    }
}
