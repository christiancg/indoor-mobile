﻿using System;
using System.Collections.ObjectModel;
using indoor.Services;
using Plugin.BluetoothLE;

namespace indoor.ViewModels.Configuration
{
	public class ConfigurationViewModel : BaseViewModel
	{
		private readonly IndoorConfigurationServices services = new IndoorConfigurationServices();

		public bool TieneLuz
		{
			get;
			set;
		}

		public bool TieneBomba
		{
			get;
			set;
		}

		public bool TieneFanIntra
		{
			get;
			set;
		}

		public bool TieneFanExtra
		{
			get;
			set;
		}

		public bool TieneHumYTemp
		{
			get;
			set;
		}

		public bool TieneCamara
		{
			get;
			set;
		}

		public string NombreIndoor
		{
			get;
			set;
		}

		public string UsuarioAdm
		{
			get;
			set;
		}

		public string PasswordAdm
		{
			get;
			set;
		}

		public ObservableCollection<IDevice> DispositivosEncontrados
		{
			get;
			set;
		}

		public ConfigurationViewModel()
		{
			services.StartScan();
			DispositivosEncontrados = services.DispositivosEncontrados;
		}

		public void StopScan()
		{
			services.StopScan();
		}
	}
}