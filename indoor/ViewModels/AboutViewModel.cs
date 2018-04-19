using System;
using System.Windows.Input;

using Xamarin.Forms;

using indoor.Services;

namespace indoor.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "About";
            OpenWebCommand = new Command(() => Device.OpenUri(new Uri("http://techhouse.com.ar/")));
        }

        public ICommand OpenWebCommand { get; }

    }
}
