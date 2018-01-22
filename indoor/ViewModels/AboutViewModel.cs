using System;
using System.Windows.Input;

using Xamarin.Forms;

using indoor.Services;

namespace indoor
{
    public class AboutViewModel : BaseViewModel
    {

        private String _TestColaResult;
        public String TestColaResult
        {
            get
            {
                return _TestColaResult;
            }
            set
            {
                _TestColaResult = value;
                OnPropertyChanged();
            }
        }

        public AboutViewModel()
        {
            Title = "About";

            OpenWebCommand = new Command(() => Device.OpenUri(new Uri("http://techhouse.com.ar/")));
            TestColaCommand = new Command(async () => {
                try
                {
                    //var rpcClient = new RpcClient();
                    //var response = await rpcClient.Call();
                    //TestColaResult = "El resultado es: " + response;
                    TestColaResult = "El resultado es: " + 123;
                }
                catch (Exception ex)
                {
                    TestColaResult = "Error de comunicacion con la cola: " + ex.Message;
                }
            });
        }

        public ICommand OpenWebCommand { get; }

        public ICommand TestColaCommand { get; }
    }
}
