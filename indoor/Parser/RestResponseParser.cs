using System;
using indoor.Models;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Globalization;

namespace indoor.Parser
{
    public static class RestResponseParser
    {
        private const String dateFormat = "dd-MM-yyyyTHH:mm:ss"; 

        public static EstadoIndoor parseEstadoIndoor(String response)
        {
            EstadoIndoor resultado = null;
            try
            {
                JArray json = JArray.Parse(response);
                Boolean estadoLuz = (from jel in json.Children() where jel["desc"].ToString() == "luz" select Boolean.Parse(jel["estado"].ToString())).FirstOrDefault();
                Boolean estadoFanIntra = (from jel in json.Children() where jel["desc"].ToString() == "fanintra" select Boolean.Parse(jel["estado"].ToString())).FirstOrDefault();
                Boolean estadoFanExtra = (from jel in json.Children() where jel["desc"].ToString() == "fanextra" select Boolean.Parse(jel["estado"].ToString())).FirstOrDefault();
                resultado = new EstadoIndoor(estadoLuz,estadoFanIntra,estadoFanExtra);
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            return resultado;
        }

        public static List<Evento> parseListaEventos(string response)
        {
            List<Evento> resultado = null;
            try
            {
                resultado = new List<Evento>();
                JArray json = JArray.Parse(response);
                foreach (var item in json.Children())
                {
                    int estadoLuz = (from jit in item select Int32.Parse(jit["id"].ToString())).FirstOrDefault();
                    DateTime fechaYHora = (from jit in item select DateTime.ParseExact(jit["fechayhora"].ToString(), dateFormat,CultureInfo.InvariantCulture)).FirstOrDefault();
                    ConfigGPIO gpio = (from jit in item select (ConfigGPIO)jit["configgpio"]["desc"].ToString()).FirstOrDefault();
                    Boolean estado = (from jit in item select Boolean.Parse(jit["configgpio"]["estado"].ToString())).FirstOrDefault();
                    String desc = (from jit in item select jit["desc"].ToString()).FirstOrDefault();
                    HumedadYTemperatura humytemp = null;
                    if (gpio.ToString() == ConfigGPIO.SENSOR_HUM_Y_TEMP.ToString())
                    {
                        JObject jhyt = JObject.Parse(desc);
                        Decimal hum = (from s in jhyt.Children() select Decimal.Parse(s["humedad"].ToString())).FirstOrDefault();
                        Decimal temp = (from s in jhyt.Children() select Decimal.Parse(s["temperatura"].ToString())).FirstOrDefault();;
                        humytemp = new HumedadYTemperatura(hum, temp);
                        resultado.Add(new Evento(fechaYHora,humytemp));
                    }
                    else
                    {
                        resultado.Add(new Evento(fechaYHora, gpio, estado, desc));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            return resultado;
        }
    }
}
