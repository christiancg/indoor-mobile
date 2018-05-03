using System;
using System.Collections.Generic;
using Acr.UserDialogs;
using indoor.Models;
using indoor.Services;
using indoor.ViewModels.Configuration.DetailViewModels;
using Xamarin.Forms;

namespace indoor.Views.Configuration.DetailPages
{
    public partial class WlanConfigPage : BaseDetailPage
    {
        private WlanConfigViewModel viewModel;

        public WlanConfigPage()
        {
            InitializeComponent();
        }

        public WlanConfigPage(IndoorConfigurationServices btServices)
        {
            InitializeComponent();
            BindingContext = viewModel = new WlanConfigViewModel(btServices);
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var evento = args.SelectedItem as Wifi;
            if (evento == null)
                return;
            viewModel.SelectedNetwork = evento;
            if (evento.SecurityType != WifiSecurityType.NONE)
            {
                PromptConfig config = new PromptConfig();
                config.InputType = InputType.Password;
                config.IsCancellable = true;
                config.SetTitle("Ingrese password");
                config.SetMessage("Ingrese el password de la red wifi " + evento.Ssid);
                PromptResult result = await UserDialogs.Instance.PromptAsync(config);
                if (result.Ok)
                {
                    evento.Password = result.Text;
                }
                else
                {
                    WifisListView.SelectedItem = null;
                    return;
                }
            }
            viewModel.ConnectToNetworkCommand.Execute(evento);
            viewModel.GetConnectedNetworkCommand.Execute(null);
            WifisListView.SelectedItem = null;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.GetConnectedNetworkCommand.Execute(null);
            viewModel.LoadNetworksCommand.Execute(null);
        }
    }
}
