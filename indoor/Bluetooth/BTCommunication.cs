using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Threading;
using System.Text;
using Plugin.BluetoothLE;
using System.Collections.Generic;

namespace indoor.Bluetooth
{
	public class BTCommunication
	{
		private bool canScan = false;
		private IDevice connectedDevice = null;
		private IDisposable deviceScanner = null;
		private Thread scanThread = null;

		public ObservableCollection<IDevice> scanResult
		{
			get;
		}

		public List<IGattService> Services
		{
			get;
			set;
		}

		public List<IGattCharacteristic> Characteristics
		{
			get;
			set;
		}

		public List<IGattDescriptor> Descriptors
		{
			get;
			set;
		}

		public BTCommunication()
		{
			scanResult = new ObservableCollection<IDevice>();
			Services = new List<IGattService>();
			Characteristics = new List<IGattCharacteristic>();
			Descriptors = new List<IGattDescriptor>();
		}

		public void Connect(IDevice toConnect)
		{
			connectedDevice = toConnect;
			connectedDevice.Connect().Subscribe(c =>
			{
				GetServicesCharacteristicsDescriptors();
			});
		}

		public bool Write(IGattCharacteristic characteristic, byte[] toWrite)
		{
			bool ok = false;
			if (characteristic.CanWrite())
			{
				bool isWriting = true;
				byte[] hasWritten = null;
				characteristic.Write(toWrite).Subscribe(written =>
				{
					hasWritten = written.Data;
					isWriting = false;
				});
				while (isWriting) { }
				ok = toWrite == hasWritten;
			}
			return ok;
		}

		public string Read(IGattCharacteristic characteristic)
		{
			string result = null;
			if (characteristic.CanRead())
			{
				characteristic.Read().Subscribe(res =>
				{
					result = Encoding.UTF8.GetString(res.Data);
				});
				while (result == null) { }
				return result;
			}
			else
				return result;
		}

		private void GetServicesCharacteristicsDescriptors()
		{
			connectedDevice.WhenServiceDiscovered().Subscribe(serv =>
			{
				Services.Add(serv);
			});
			connectedDevice.WhenAnyCharacteristicDiscovered().Subscribe(charac =>
			{
				Characteristics.Add(charac);
			});
			connectedDevice.WhenAnyDescriptorDiscovered().Subscribe(descr =>
			{
				Descriptors.Add(descr);
			});
		}

		public void StartScanning()
		{
			scanThread = new Thread(() =>
			{
				while (CrossBleAdapter.Current.Status == AdapterStatus.Unknown) { }
				if (CrossBleAdapter.Current.Status != AdapterStatus.Unsupported)
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
			}
		}
	}
}
