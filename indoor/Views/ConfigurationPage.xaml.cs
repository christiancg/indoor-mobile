using System;
using System.Linq;
using System.Collections.ObjectModel;

using Xamarin.Forms;

using indoor.ViewModels;
using Plugin.BluetoothLE;

namespace indoor.Views
{
    public partial class ConfigurationPage : ContentPage
    {

        private ConfigurationViewModel viewModel = null;
        private ObservableCollection<IScanResult> scanResult = null;
        private IDevice selectedDevice = null;
        private IObservable<object> connectedDevice = null;

        public ConfigurationPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new ConfigurationViewModel();
            grilla.IsVisible = false;
            scanResult = new ObservableCollection<IScanResult>();
            listView.ItemsSource = scanResult;
            StartScanning();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        public void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (((ListView)sender).SelectedItem == null)
            {
                return;
            }
            IsBusy = false;

            listView.IsVisible = false;
            grilla.IsVisible = true;
            IScanResult sr = e.SelectedItem as IScanResult;
            selectedDevice = sr.Device;
            GattConnectionConfig config = new GattConnectionConfig();
            config.Priority = ConnectionPriority.High;
            GetCharacteristics();
            connectedDevice = selectedDevice.Connect(config);


            ((ListView)sender).SelectedItem = null; // clear selection
        }

        void StartScanning()
        {
            CrossBleAdapter.Current.ScanWhenAdapterReady().Subscribe(encontrado =>
            {
                var found = (from x in scanResult where x.Device.Name == encontrado.Device.Name select x).FirstOrDefault();
                if (found == null)
                    this.scanResult.Add(encontrado);
            });
        }

        void GetCharacteristics()
        {
            selectedDevice.WhenAnyCharacteristicDiscovered().Subscribe(charac =>
            {

            });
            selectedDevice.WhenAnyDescriptorDiscovered().Subscribe(descr =>
            {

            });
            selectedDevice.WhenServiceDiscovered().Subscribe(serv =>
            {
                
            });
        }

        protected override void OnDisappearing()
        {
            scanResult.Clear();
        }

        async void Save(object sender, EventArgs e)
        {

        }


    }
}
