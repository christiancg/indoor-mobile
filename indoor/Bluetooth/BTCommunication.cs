using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Threading;
using Plugin.BluetoothLE;
using System.Collections.Generic;

namespace indoor.Bluetooth
{
	public class BTCommunication
	{
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

		public TransactionStatus Write(IGattCharacteristic characteristic, byte[] toWrite)
		{
			TransactionStatus status = TransactionStatus.Aborted;
			using (var trans = connectedDevice.BeginReliableWriteTransaction())
			{
				trans.Write(characteristic, toWrite);
				// you should do multiple writes here as that is the reason for this mechanism
				trans.Commit();
				status = trans.Status;
			}
			return status;
		}

		public IObservable<CharacteristicResult> Read(IGattCharacteristic characteristic, byte[] toWrite)
		{
			IObservable<CharacteristicResult> result = null;
			if (characteristic.CanRead())
				result = characteristic.Read();
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
				deviceScanner = CrossBleAdapter.Current.Scan().Subscribe(encontrado =>
				{
					var found = (from x in scanResult where x.Uuid == encontrado.Device.Uuid select x).FirstOrDefault();
					if (found == null)
					{
						this.scanResult.Add(encontrado.Device);
					}
				});
			});
			scanThread.Start();
		}

		public void StopScanning()
		{
			deviceScanner.Dispose();
			scanThread.Abort();
		}
	}
}
