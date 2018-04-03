using System;
using System.Linq;
using System.Collections.ObjectModel;

using Xamarin.Forms;

using indoor.ViewModels;
using Plugin.BluetoothLE;
using System.Text;
using System.Threading;

namespace indoor.Views
{
    public partial class ConfigurationPage : ContentPage
    {

        private ConfigurationViewModel viewModel = null;
        private ObservableCollection<IScanResult> scanResult = null;
        private IDevice selectedDevice = null;
        private IObservable<object> connectedDevice = null;
        private IDisposable deviceScanner = null;
        private Thread scanThread = null;

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
            selectedDevice.Connect().Subscribe( c => {
                GetCharacteristics();    
            });
            //Write();
            ((ListView)sender).SelectedItem = null; // clear selection
        }

        private async void Write()
        {
            using (var trans = selectedDevice.BeginReliableWriteTransaction())
            {
                string bytes = "hola";
                trans.Write(null, Encoding.Default.GetBytes(bytes));
                // you should do multiple writes here as that is the reason for this mechanism
                trans.Commit();
            }
        }

        void StartScanning()
        {
            scanThread = new Thread(() =>
            {
                while (CrossBleAdapter.Current.Status == AdapterStatus.Unknown) { }
                deviceScanner = CrossBleAdapter.Current.Scan().Subscribe(encontrado =>
                {
                    var found = (from x in scanResult where x.Device.Name == encontrado.Device.Name select x).FirstOrDefault();
                    if (found == null)
                        this.scanResult.Add(encontrado);
                });
            });
            scanThread.Start();
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
            deviceScanner.Dispose();
            scanThread.Abort();
            scanResult = null;
        }

        async void Save(object sender, EventArgs e)
        {

        }


    }
}
