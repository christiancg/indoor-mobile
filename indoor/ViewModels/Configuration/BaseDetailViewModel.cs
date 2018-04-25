using System;
using indoor.Models;
using indoor.Views.Configuration.DetailPages;
using Xamarin.Forms;

namespace indoor.ViewModels.Configuration
{
	public class BaseDetailViewModel : BaseViewModel
	{
		public BaseDetailViewModel()
		{
		}

		protected void SendMessage(Alert alert)
		{
			MessagingCenter.Send<BaseDetailViewModel, Alert>(this, "MostrarMensaje", alert);
		}
	}
}
