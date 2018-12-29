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

        public void ShowActionSheetAsync(string title, string CancelbuttonLabel, string[] items)
        {
            ActionSheetConfig cfg = new ActionSheetConfig();
            foreach(string item in items)
            {
                cfg.Add(item);
            }
            cfg.SetTitle(title);
            UserDialogs.Instance.ActionSheet(cfg);
           
        }
    }
}
