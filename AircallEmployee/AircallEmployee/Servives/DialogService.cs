using Acr.UserDialogs;
using AircallEmployee.Servives.Interfaces;
using System.Threading.Tasks;

namespace AircallEmployee.Servives
{
    public class DialogService : IDialogService
    {
        public Task ShowAlertAsync(string message, string title, string buttonLabel)
        {
            return UserDialogs.Instance.AlertAsync(message, title, buttonLabel);
        }

        public Task ShowPromptAsync(string message, string title, string OKbuttonLabel, string CancelbuttonLabel)
        {
            return UserDialogs.Instance.PromptAsync(message, title, OKbuttonLabel, CancelbuttonLabel);
        }
    }
}
