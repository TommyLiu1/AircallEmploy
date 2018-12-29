using AircallEmployee.PageModels.Base;
using System.Windows.Input;
using Xamarin.Forms;

namespace AircallEmployee.PageModels
{
    public class HomePageModel : PageModelBase
    {
        public ICommand SetScheduleViewCommand => new Command(SetScheduleView);

        private  void SetScheduleView()
        {
            string[] items = { "week", "month,today" };
             DialogService.ShowActionSheetAsync("Schedule View", "cacel", items);
        }
    }
}
