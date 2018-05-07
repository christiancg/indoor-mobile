using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Threading;
using System.Text;
using Plugin.BluetoothLE;
using System.Collections.Generic;
using indoor.Services;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace indoor.Bluetooth
{
	public class BTCommunication
	{
		private readonly int timeoutMilliseconds = 5000;
		private IDevice connectedDevice = null;

		public ObservableCollection<IDevice> ScanResult
		{
			get;
		}

		public ObservableCollection<IGattService> Services
		{
			get;
			set;
		}

		public ObservableCollection<IGattCharacteristic> Characteristics
		{
			get;
			set;
		}

		public BTCommunication()
		{
			if (CrossBleAdapter.Current.Status == AdapterStatus.PoweredOff)
				CrossBleAdapter.Current.SetAdapterState(true);
			ScanResult = new ObservableCollection<IDevice>();
			Services = new ObservableCollection<IGattService>();
			Characteristics = new ObservableCollection<IGattCharacteristic>();
		}

		public void Connect(IDevice toConnect)
		{
			connectedDevice = toConnect;
			connectedDevice.Connect(new ConnectionConfig
			{
				AutoConnect = false,
				AndroidConnectionPriority = ConnectionPriority.High            
			});
		}

		public void Disconnect()
		{
			if (CrossBleAdapter.Current.IsScanning)
				CrossBleAdapter.Current.StopScan();
			connectedDevice.CancelConnection();
		}

		public Task<bool> Write(Guid service, Guid characteristic, byte[] toWrite) => Task.Run(() =>
		{
			bool hasWritten = false;
			BlockingCollection<bool> bcResp = new BlockingCollection<bool>();
			connectedDevice.WriteCharacteristic(service, characteristic, toWrite).Subscribe(written =>
				 {
					 bcResp.Add(true);
				 });
			bcResp.TryTake(out hasWritten, timeoutMilliseconds);
			return hasWritten;
		});

		public Task<string> Read(Guid service, Guid characteristic) => Task.Run(() =>
			{
				string result = null;
				BlockingCollection<string> bcResp = new BlockingCollection<string>();
				connectedDevice.ReadCharacteristic(service, characteristic).Subscribe(res =>
				{
					result = Encoding.UTF8.GetString(res.Data);
					bcResp.Add(result);
				});
				bcResp.TryTake(out result, timeoutMilliseconds);
				return result;
			});

		public void StartScanning()
		{
			if (CrossBleAdapter.Current.Status == AdapterStatus.Unsupported || CrossBleAdapter.Current.Status == AdapterStatus.Unauthorized)
				return;
			if (CrossBleAdapter.Current.Status == AdapterStatus.PoweredOn)
			{
				Scan();
			}
			else
			{
				CrossBleAdapter.Current.WhenStatusChanged().Subscribe(newStatus =>
				{
					if (newStatus == AdapterStatus.PoweredOn)
						Scan();
				});
			}
		}

		private void Scan()
		{
			CrossBleAdapter.Current.ScanForUniqueDevices().Subscribe(encontrado =>
			{
				if (encontrado.Name.Contains("indoor"))
					ScanResult.Add(encontrado);
			});
		}

		public void StopScanning()
		{
			if (CrossBleAdapter.Current.IsScanning)
				CrossBleAdapter.Current.StopScan();
		}
	}
}
