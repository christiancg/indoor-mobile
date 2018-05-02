using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using indoor.Models;
using indoor.Services;
using Xamarin.Forms;

namespace indoor.ViewModels.Configuration.DetailViewModels
{
    public class WlanConfigViewModel : BaseDetailViewModel
    {
        public ObservableCollection<Wifi> Networks
        {
            get;
            set;
        } = new ObservableCollection<Wifi>();

        private string _ConnectedNetwork;
        public string ConnectedNetwork
        {
            get
            {
                return _ConnectedNetwork;
            }
            set
            {
                string prefijo = "Conectado a: ";
                if (value == null || value == "")
                    _ConnectedNetwork = prefijo + "Ninguno";
                else
                    _ConnectedNetwork = prefijo + value;
                OnPropertyChanged();
            }
        }

        public Wifi SelectedNetwork
        {
            get;
            set;
        }

        private readonly IndoorConfigurationServices btServices;

        public Command LoadNetworksCommand
        {
            get;
            set;
        }

        public Command ConnectToNetworkCommand
        {
            get;
            set;
        }

        public Command GetConnectedNetworkCommand
        {
            get;
            set;
        }

        public WlanConfigViewModel(IndoorConfigurationServices btServices)
        {
            this.btServices = btServices;
            this.LoadNetworksCommand = new Command(async () => await LoadNetworks());
            this.ConnectToNetworkCommand = new Command(async () => await ConnectToNetwork());
            this.GetConnectedNetworkCommand = new Command(async () => await GetConnectedNetwork());
        }

        private async Task LoadNetworks()
        {
            if (IsBusy)
                return;
            IsBusy = true;
            List<Wifi> auxNetworks = await btServices.GetWifiNetworks();
            if (auxNetworks != null)
            {
                this.Networks.Clear();
                foreach (var item in auxNetworks)
                {
                    this.Networks.Add(item);
                }
            }
            IsBusy = false;
        }

        private async Task ConnectToNetwork()
        {
            if (this.SelectedNetwork != null)
            {
                bool result = await this.btServices.ConnectToWifi(SelectedNetwork.Ssid, SelectedNetwork.Password);
                Alert alert = null;
                if (result)
                    alert = new Alert("Conexion exitosa", "El indoor se ha conectado exitosamente a la red wifi " + SelectedNetwork.Ssid);
                else
                    alert = new Alert("Error al conectarse", "Ocurrio un error al conectarse a la red wifi");
                SendMessage(alert);
            }
        }

        private async Task GetConnectedNetwork()
        {
            this.ConnectedNetwork = await this.btServices.GetConnectedWifi();
        }
    }
}
