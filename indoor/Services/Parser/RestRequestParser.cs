using System;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json.Linq;
using indoor.Models;

namespace indoor.Services.Parser
{
    public static class RestRequestParser
    {
        private const String timeFormat = "HH:mm:ss";

        public static StringContent parseAgregarProgramacionRequest(Programacion toParse)
        {
            StringContent result = null;
            try
            {
                JObject auxR = new JObject();
                auxR["desc"] = toParse.descripcion;
                auxR["prender"] = toParse.prender;
                auxR["horario1"] = toParse.hora1.ToString(timeFormat);
                if(toParse.hora2 != TimeSpan.Zero)
                    auxR["horario2"] = toParse.hora2.ToString(timeFormat);
                auxR["configgpio"] = toParse.gpio.ToString();
                result = new StringContent(auxR.ToString(), Encoding.UTF8, "application/json");
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            return result;
        }
    }
}
