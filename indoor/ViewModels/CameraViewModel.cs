using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace indoor.ViewModels
{
    public class CameraViewModel : BaseViewModel
    {
        private bool recargandoImagen;

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

        public Command RecargarImagenCommand { get; set; }

        public CameraViewModel()
        {
            RecargarImagenCommand = new Command(async () => await ExecuteRecargarImagenCommand());
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
