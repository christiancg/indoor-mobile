using System;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;
using Xamarin.Forms;

using indoor.Models;

namespace indoor.ViewModels
{
    public class StatusViewModel : BaseViewModel
    {
        private decimal _Humedad;
        public decimal Humedad
        {
            get
            {
                return _Humedad;
            }
            set
            {
                _Humedad = value;
                OnPropertyChanged();
            }
        }

        private decimal _Temperatura;
        public decimal Temperatura
        {
            get
            {
                return _Temperatura;
            }
            set
            {
                _Temperatura = value;
                OnPropertyChanged();
            }
        }

        private bool _Luz;
        public bool Luz
        {
            get
            {
                return _Luz;
            }
            set
            {
                _Luz = value;
                OnPropertyChanged();
            }
        }

        private bool _FanIntra;
        public bool FanIntra
        {
            get
            {
                return _FanIntra;
            }
            set
            {
                _FanIntra = value;
                OnPropertyChanged();
            }
        }

        private bool _FanExtra;
        public bool FanExtra
        {
            get
            {
                return _FanExtra;
            }
            set
            {
                _FanExtra = value;
                OnPropertyChanged();
            }
        }

        private int _CantSegundos;
        public int CantSegundos
        {
            get
            {
                return _CantSegundos;
            }
            set
            {
                _CantSegundos = value;
                OnPropertyChanged();
            }
        }

        private String _TxtCantSegundos;
        public String TxtCantSegundos
        {
            get
            {
                return _TxtCantSegundos;
            }
            set
            {
                _TxtCantSegundos = value;
                OnPropertyChanged();
            }
        }

        public bool TieneHumYTemp
        {
            get;
            set;
        }

        public bool TieneLuz
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

        public bool TieneBomba
        {
            get;
            set;
        }

        private bool regando;

        private List<ConfigGPIO> configs = null;

        public Command GetEstadoCommand { get; set; }

        public Command RegarCommand { get; set; }

        public StatusViewModel(List<ConfigGPIO> configs)
        {
            GetEstadoCommand = new Command(async () => await ExecuteLoadEstadoCommand());
            RegarCommand = new Command(async () => await ExecuteRegarCommand());
            this.configs = configs;
            EsconderCosas();
            CantSegundos = 30;
            TxtCantSegundos = "Se regará " + CantSegundos + " segundos";
        }

        private void EsconderCosas()
        {
            TieneBomba = configs.Contains(ConfigGPIO.BOMBA);
            TieneLuz = configs.Contains(ConfigGPIO.LUZ);
            TieneFanExtra = configs.Contains(ConfigGPIO.FAN_EXTRA);
            TieneFanIntra = configs.Contains(ConfigGPIO.FAN_INTRA);
            TieneHumYTemp = configs.Contains(ConfigGPIO.SENSOR_HUM_Y_TEMP);
        }

        async Task ExecuteLoadEstadoCommand()
        {
            if (IsBusy)
                return;
            IsBusy = true;
            try
            {
                var estado = await DataStore.GetEstado();
                Luz = estado.luz;
                FanExtra = estado.fanExtra;
                FanIntra = estado.fanIntra;
                var humYTemp = await DataStore.GetHumedadYTemperatura();
                Humedad = humYTemp.humedad;
                Temperatura = humYTemp.temperatura;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        async Task ExecuteRegarCommand()
        {
            if (regando)
                return;
            regando = true;
            try
            {
                var answer = await Application.Current.MainPage.DisplayAlert("Regar", "Esta seguro que desea regar " + CantSegundos + " segundos?", "Si", "No");
                if (answer)
                {
                    var estado = await DataStore.RegarSegundos(CantSegundos);
                    if (estado)
                        await Application.Current.MainPage.DisplayAlert("Regar", "Se regó " + CantSegundos + " segundos", "Ok");
                    else
                        await Application.Current.MainPage.DisplayAlert("Regar", "Error al regar " + CantSegundos + " segundos", "Ok");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                regando = false;
            }
        }

        public void setMensajes()
        {
            MessagingCenter.Subscribe<StatusPage>(this, "CambiarTextoLabel", (obj) =>
            {
                TxtCantSegundos = "Se regará " + CantSegundos + " segundos";
            });
        }

        public void unsetMensajes()
        {
            MessagingCenter.Unsubscribe<StatusPage>(this, "CambiarTextoLabel");
        }

    }
}
