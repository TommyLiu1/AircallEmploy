using System.Threading.Tasks;

namespace AircallEmployee.Servives.Interfaces
{
    public interface IDialogService
    {
        Task ShowAlertAsync(string message, string title, string buttonLabel);
        Task ShowPromptAsync(string message, string title, string OKbuttonLabel,string CancelbuttonLabel);
    }
}
