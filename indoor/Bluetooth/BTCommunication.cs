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

		public BTCommunication()
		{
			if (CrossBleAdapter.Current.Status == AdapterStatus.PoweredOff)
				CrossBleAdapter.Current.SetAdapterState(true);
			ScanResult = new ObservableCollection<IDevice>();
		}

		public void Connect(Guid toConnect)
		{
			StopScanning();
			connectedDevice = CrossBleAdapter.Current.GetKnownDevice(toConnect);
			if (!connectedDevice.IsConnected())
				connectedDevice.Connect(new ConnectionConfig
				{
					AutoConnect = false,
					AndroidConnectionPriority = ConnectionPriority.High
				});
		}

		public void Disconnect()
		{
			if (CrossBleAdapter.Current.IsScanning)
				scan.Dispose();
			connectedDevice.CancelConnection();
		}

		private IDisposable writeDisposable;

		public Task<bool> Write(Guid service, Guid characteristic, byte[] toWrite) => Task.Run(() =>
		{
			bool hasWritten = false;
			BlockingCollection<bool> bcResp = new BlockingCollection<bool>();
			writeDisposable = connectedDevice.WriteCharacteristic(service, characteristic, toWrite).Subscribe(written =>
				 {
					 bcResp.Add(true);
				 });
			bcResp.TryTake(out hasWritten, timeoutMilliseconds);
			writeDisposable.Dispose();
			return hasWritten;
		});

		private IDisposable readDisposable;

		public Task<string> Read(Guid service, Guid characteristic) => Task.Run(() =>
			{
				string result = null;
				BlockingCollection<string> bcResp = new BlockingCollection<string>();
				readDisposable = connectedDevice.ReadCharacteristic(service, characteristic).Subscribe(res =>
				{
					result = Encoding.UTF8.GetString(res.Data);
					bcResp.Add(result);
				});
				bcResp.TryTake(out result, timeoutMilliseconds);
				readDisposable.Dispose();
				return result;
			});

		public void StartScanning()
		{
			if (CrossBleAdapter.Current.Status == AdapterStatus.Unsupported || CrossBleAdapter.Current.Status == AdapterStatus.Unauthorized)
				return;
			statusChanged = CrossBleAdapter.Current.WhenStatusChanged().Subscribe(newStatus =>
			{
				if (newStatus == AdapterStatus.PoweredOn)
				{
					Scan();
					statusChanged.Dispose();
				}
			});
		}

		private IDisposable scan;
		private IDisposable statusChanged;

		private void Scan()
		{
			scan = CrossBleAdapter.Current.ScanForUniqueDevices(new ScanConfig()
			{
				ServiceUuids = { IndoorConfigurationServices.Instance.configurationServiceGuid, IndoorConfigurationServices.Instance.startStopRestartServiceGuid, IndoorConfigurationServices.Instance.wlanServiceGuid }
			}).Subscribe(encontrado =>
			{
				ScanResult.Add(encontrado);
			});
		}

		public void StopScanning()
		{
			if (CrossBleAdapter.Current.IsScanning)
			{
				for (int i = 0; i < ScanResult.Count; i++)
					ScanResult[i] = null;
				ScanResult.Clear();
				scan.Dispose();
			}
		}
	}
}
