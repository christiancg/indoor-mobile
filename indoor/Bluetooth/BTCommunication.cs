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
		private readonly int timeoutMilliseconds = 15000;
		private bool canScan = false;
		private IDevice connectedDevice = null;
		private IDisposable deviceScanner = null;
		private Thread scanThread = null;

		public ObservableCollection<IDevice> scanResult
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
			scanResult = new ObservableCollection<IDevice>();
			Services = new ObservableCollection<IGattService>();
			Characteristics = new ObservableCollection<IGattCharacteristic>();
		}

		public void Connect(IDevice toConnect)
		{
			connectedDevice = toConnect;
			connectedDevice.Connect();
		}

		public void Disconnect()
		{
			if (CrossBleAdapter.Current.IsScanning)
				CrossBleAdapter.Current.StopScan();
			CrossBleAdapter.Current.SetAdapterState(false);
			connectedDevice.CancelConnection();
			CrossBleAdapter.Current.SetAdapterState(true);
		}

		public Task<bool> Write(Guid service, Guid characteristic, byte[] toWrite) => Task.Run(() =>
		{
			byte[] hasWritten = null;
			BlockingCollection<byte[]> bcResp = new BlockingCollection<byte[]>();
			connectedDevice.WriteCharacteristic(service, characteristic, toWrite).Subscribe(written =>
				 {
					 bcResp.Add(written.Data);
				 });
			bcResp.TryTake(out hasWritten, timeoutMilliseconds);
			return toWrite == hasWritten;
		});

		public Task<string> Read(Guid service, Guid characteristic) => Task.Run(() =>
			{
				string result = null;
				BlockingCollection<string> bcResp = new BlockingCollection<string>();
				connectedDevice.ReadCharacteristic(service, characteristic).Subscribe(res =>
				{
					result = Encoding.UTF8.GetString(res.Data);
				});
				bcResp.TryTake(out result, timeoutMilliseconds);
				return result;
			});

		public void StartScanning()
		{
			scanThread = new Thread(() =>
			{
				while (CrossBleAdapter.Current.Status == AdapterStatus.Unknown) { }
				if (CrossBleAdapter.Current.Status != AdapterStatus.Unsupported && CrossBleAdapter.Current.Status != AdapterStatus.PoweredOff)
				{
					canScan = true;
					deviceScanner = CrossBleAdapter.Current.Scan().Subscribe(encontrado =>
					{
						var found = (from x in scanResult where x.Uuid == encontrado.Device.Uuid select x).FirstOrDefault();
						if (found == null)
						{
							this.scanResult.Add(encontrado.Device);
						}
					});
				}
			});
			scanThread.Start();
		}

		public void StopScanning()
		{
			if (canScan)
			{
				deviceScanner.Dispose();
				scanThread.Abort();
				if (CrossBleAdapter.Current.IsScanning)
					CrossBleAdapter.Current.StopScan();
			}
		}
	}
}
