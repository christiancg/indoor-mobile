using System;
using System.Collections.ObjectModel;
using Robotics.Mobile.Core.Bluetooth.LE;

using Xamarin.Forms;

using indoor.ViewModels;
using System.Diagnostics;
using System.Threading.Tasks;

namespace indoor.Views
{
    public partial class ConfigurationPage : ContentPage
    {

        private ConfigurationViewModel viewModel = null;
        private IAdapter adapter = null;
        private IDevice device = null;
        private ObservableCollection<IService> services = null;
        private ObservableCollection<IDevice> devices = null;
        private ObservableCollection<ICharacteristic> characteristics = null;

        public ConfigurationPage(IAdapter adapter)
        {
            InitializeComponent();
            this.adapter = adapter;
            SetUpBluetooth();
            BindingContext = viewModel = new ConfigurationViewModel();
            grilla.IsVisible = false;
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private void SetUpBluetooth()
        {
            this.devices = new ObservableCollection<IDevice>();
            listView.ItemsSource = devices;

            adapter.DeviceDiscovered += (object sender, DeviceDiscoveredEventArgs e) =>
            {
                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    devices.Add(e.Device);
                });
            };

            adapter.ScanTimeoutElapsed += (sender, e) =>
            {
                adapter.StopScanningForDevices(); // not sure why it doesn't stop already, if the timeout elapses... or is this a fake timeout we made?
                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    IsBusy = false;
                    DisplayAlert("Timeout", "Bluetooth scan timeout elapsed, no heart rate monitors were found", "OK");
                });
            };

            // when device is connected
            adapter.DeviceConnected += (s, e) =>
            {
                //device = e.Device; // do we need to overwrite this?
                // when services are discovered
                device.ServicesDiscovered += (object se, EventArgs ea) =>
                {
                    Debug.WriteLine("device.ServicesDiscovered");
                    //services = (List<IService>)device.Services;
                    if (services.Count == 0)
                        Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                        {
                            foreach (var service in device.Services)
                            {
                                services.Add(service);
                            }
                        });
                    IsBusy = false;
                };
                IsBusy = true;
                // start looking for services
                device.DiscoverServices();
            };
            StartScanning();
        }

        public void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (((ListView)sender).SelectedItem == null)
            {
                return;
            }
            Debug.WriteLine(" xxxxxxxxxxxx  OnItemSelected " + e.SelectedItem.ToString());
            IsBusy = false;
            StopScanning();

            listView.IsVisible = false;
            grilla.IsVisible = true;

            device = e.SelectedItem as IDevice;
            this.services = new ObservableCollection<IService>();
            adapter.ConnectToDevice(device);

            //var servicePage = new ServiceList(adapter, device);
            // load services on the next page
            //Navigation.PushAsync(servicePage);

            ((ListView)sender).SelectedItem = null; // clear selection
        }

        void StartScanning()
        {
            IsBusy = true;
            StartScanning(Guid.Empty);
        }

        void StartScanning(Guid forService)
        {
            if (adapter.IsScanning)
            {
                IsBusy = false;
                adapter.StopScanningForDevices();
                Debug.WriteLine("adapter.StopScanningForDevices()");
            }
            else
            {
                devices.Clear();
                IsBusy = true;
                adapter.StartScanningForDevices(forService);
                Debug.WriteLine("adapter.StartScanningForDevices(" + forService + ")");
            }
        }

        void StopScanning()
        {
            // stop scanning
            new Task(() =>
            {
                if (adapter.IsScanning)
                {
                    Debug.WriteLine("Still scanning, stopping the scan");
                    adapter.StopScanningForDevices();
                    IsBusy = false;
                }
            }).Start();
        }

        async void Save(object sender, EventArgs e)
        {
            byte[] infoToSend = new byte[1024];
            characteristics[0].Write(infoToSend);
            var result = await characteristics[0].ReadAsync();
            var byteResult = result.Value;
        }


    }
}
