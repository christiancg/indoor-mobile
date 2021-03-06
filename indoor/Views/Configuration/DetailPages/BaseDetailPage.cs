﻿using System;
using indoor.Models;
using indoor.ViewModels.Configuration;
using Xamarin.Forms;

namespace indoor.Views.Configuration.DetailPages
{
	public class BaseDetailPage : ContentPage
	{
		public RequiresRestart RequiresRestart
		{
			get;
			set;
		}

		public BaseDetailPage()
		{
			RequiresRestart = RequiresRestart.NO;
		}

		protected override void OnAppearing()
		{
			MessagingCenter.Subscribe<BaseDetailViewModel, Alert>(this, "MostrarMensaje", (obj, alertObj) =>
			{
				DisplayAlert(alertObj.Titulo, alertObj.Mensaje, alertObj.TextoBoton);
			});
			MessagingCenter.Subscribe<BaseDetailViewModel, RequiresRestart>(this, "CambiarRequiresRestart", (obj, reqRes) =>
            {
				RequiresRestart = reqRes;
            });
		}

		protected override void OnDisappearing()
		{
			base.OnDisappearing();
			MessagingCenter.Unsubscribe<BaseDetailViewModel>(this, "MostrarMensaje");
			MessagingCenter.Unsubscribe<BaseDetailViewModel>(this, "CambiarRequiresRestart");
		}
	}
}
