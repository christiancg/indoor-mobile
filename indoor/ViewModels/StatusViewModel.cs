using System;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using Xamarin.Forms;

namespace indoor.ViewModels
{
    public class StatusViewModel : BaseViewModel
    {
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

        private ImageSource _Image;
        public ImageSource Image
        {
            get
            {
                return _Image;
            }
            set
            {
                _Image = value;
                OnPropertyChanged();
            }
        }

        private bool regando;

        public Command GetEstadoCommand { get; set; }

        public Command RegarCommand { get; set; }

        public Command RecargarImagenCommand { get; set; }

        private bool recargandoImagen;

        public StatusViewModel()
        {
            GetEstadoCommand = new Command(async () => await ExecuteLoadEstadoCommand());
            RegarCommand = new Command(async () => await ExecuteRegarCommand());
            RecargarImagenCommand = new Command(async () => await ExecuteRecargarImagenCommand());

            CantSegundos = 30;
            TxtCantSegundos = "Se regará " + CantSegundos + " segundos";

            MessagingCenter.Subscribe<StatusPage>(this, "CambiarTextoLabel", (obj) =>
            {
                TxtCantSegundos = "Se regará " + CantSegundos + " segundos";
            });
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

        async Task ExecuteRecargarImagenCommand()
        {
            if (recargandoImagen)
                return;
            recargandoImagen = true;
            try
            {
                var imagenIndoor = await DataStore.ObtenerImagen();
                if (imagenIndoor.EstadoTomaImagen)
                    Image = ImageSource.FromStream(() => new MemoryStream(System.Convert.FromBase64String(imagenIndoor.B64Image)));
                else
                    await Application.Current.MainPage.DisplayAlert("Obtener imagen", "Error al obtener imagen del indoor: " + imagenIndoor.B64Image, "Ok");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Application.Current.MainPage.DisplayAlert("Obtener imagen", "Error al obtener imagen del indoor: " + ex, "Ok");
            }
            finally
            {
                recargandoImagen = false;
            }
        }

    }
}
