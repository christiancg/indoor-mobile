using System;
using System.Diagnostics.Contracts;
using indoor.Config;
using indoor.Services.Implementation;
namespace indoor.Services
{
    public static class IndoorComunicaitionFactory
    {
        public static IIndoorComunicationService GetInstance(){
            Contract.Ensures(Contract.Result<IIndoorComunicationService>() != null);
            if (Configuracion.Instancia.useRestComunicationSchema)
                return new IndoorRestService();
            else
                return new IndoorQueueService();
        }
    }
}
