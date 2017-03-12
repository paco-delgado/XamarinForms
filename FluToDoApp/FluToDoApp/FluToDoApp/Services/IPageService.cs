using System.Threading.Tasks;
using Xamarin.Forms;

namespace FluToDoApp.Services
{
    public interface IPageService
    {
        Task DisplayAlertAsync(string title, string message, string ok);
        Task<bool> DisplayAlertAsync(string title, string message, string ok, string cancel);
        Task<Page> PopAsync();
        Task PushAsync(Page page);
    }
}