using System;
using System.Collections.Generic;

using indoor.ViewModels;
using indoor.Config;

using Xamarin.Forms;

namespace indoor.Views
{
	public partial class ConnectionPage : ContentPage
	{

		ConnectionViewModel viewModel;

		public ConnectionPage()
		{
			InitializeComponent();
			BindingContext = viewModel = new ConnectionViewModel(this.Navigation); // HERE
			ConfiguracionSaverRetriever.RetrieveProperties();
			string urlOrIndoorName = !Configuracion.Instancia.useRestComunicationSchema ? (Configuracion.Instancia.restBaseUrl.Contains("-") ? Configuracion.Instancia.restBaseUrl.Split('-')[1] : Configuracion.Instancia.restBaseUrl) : Configuracion.Instancia.restBaseUrl;
			this.txtURL.Text = urlOrIndoorName;
			this.txtUsuario.Text = Configuracion.Instancia.usuario;
			this.txtPassword.Text = Configuracion.Instancia.contrasenia;
			this.swRest.IsToggled = Configuracion.Instancia.useRestComunicationSchema;
			this.swRemember.IsToggled = Configuracion.Instancia.saveConfiguration;
		}

		void OnClick(object sender, EventArgs e)
		{
			MessagingCenter.Send(this, "LogIn");
		}

		void AbrirConfiguracion(object sender, EventArgs e)
		{
			MessagingCenter.Send(this, "Configurar");
		}

		protected override void OnAppearing()
		{
			viewModel.setMensajes();
		}

		protected override void OnDisappearing()
		{
			viewModel.unsetMensajes();
		}

	}
}
