using System;
using indoor.Models;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Globalization;

namespace indoor.Services.Parser
{
    public static class RestResponseParser
    {
        private const String dateFormat = "dd-MM-yyyyTHH:mm:ss";
        private const String timeFormat = "HH:mm:ss";

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
                    //int id = (from jit in item select Int32.Parse(jit["id"].ToString())).FirstOrDefault();
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

        public static List<Programacion> parseListaProgramaciones(string response)
        {
            List<Programacion> resultado = null;
            try
            {
                resultado = new List<Programacion>();
                JArray json = JArray.Parse(response);
                foreach (var item in json.Children())
                {
                    //int id = (from jit in item select Int32.Parse(jit["id"].ToString())).FirstOrDefault();
                    Boolean habilitado = (from jit in item select Boolean.Parse(jit["habilitado"].ToString())).FirstOrDefault();
                    Boolean prender = (from jit in item select Boolean.Parse(jit["prender"].ToString())).FirstOrDefault();
                    TimeSpan horario1 = (from jit in item select TimeSpan.ParseExact(jit["horario1"].ToString(), timeFormat, CultureInfo.InvariantCulture)).FirstOrDefault();

                    ConfigGPIO gpio = (from jit in item select (ConfigGPIO)jit["configgpio"]["desc"].ToString()).FirstOrDefault();
                    Boolean estado = (from jit in item select Boolean.Parse(jit["configgpio"]["estado"].ToString())).FirstOrDefault();
                    String desc = (from jit in item select jit["desc"].ToString()).FirstOrDefault();
                    if (item.Children()["horario2"] != null)
                    {
                        TimeSpan horario2 = (from jit in item select TimeSpan.ParseExact(jit["horario2"].ToString(), timeFormat, CultureInfo.InvariantCulture)).FirstOrDefault();
                        resultado.Add(new Programacion(gpio,horario1,horario2, prender,desc,habilitado));
                    }
                    else
                    {
                        resultado.Add(new Programacion(gpio, horario1, prender, desc, habilitado));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            return resultado;
        }

        public static HumedadYTemperatura parseHumedadYTemperatura(String response)
        {
            HumedadYTemperatura resultado = null;
            try
            {
                JObject json = JObject.Parse(response);
                Decimal humedad = (from jel in json.Children() select Decimal.Parse(jel["humedad"].ToString())).FirstOrDefault();
                Decimal temperatura = (from jel in json.Children() select Decimal.Parse(jel["temperatura"].ToString())).FirstOrDefault();
                resultado = new HumedadYTemperatura(humedad, temperatura);
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            return resultado;
        }
    }
}
