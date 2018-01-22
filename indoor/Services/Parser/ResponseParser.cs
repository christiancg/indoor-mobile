using System;
using indoor.Models;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Globalization;

namespace indoor.Services.Parser
{
    public static class ResponseParser
    {
        //private const String dateFormat = "yyyy-MM-ddTHH:mm:ss";
        //private const String timeFormat = "HH:mm:ss";

        public static EstadoIndoor parseEstadoIndoor(String response)
        {
            EstadoIndoor resultado = null;
            try
            {
                JArray json = JArray.Parse(response);
                Boolean estadoLuz = (from jel in json.Children() where jel["desc"].ToString() == "luz" select Boolean.Parse(jel["estado"].ToString())).FirstOrDefault();
                Boolean estadoFanIntra = (from jel in json.Children() where jel["desc"].ToString() == "fanintra" select Boolean.Parse(jel["estado"].ToString())).FirstOrDefault();
                Boolean estadoFanExtra = (from jel in json.Children() where jel["desc"].ToString() == "fanextra" select Boolean.Parse(jel["estado"].ToString())).FirstOrDefault();
                resultado = new EstadoIndoor(estadoLuz, estadoFanIntra, estadoFanExtra);
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
                    DateTime fechaYHora = item["fechayhora"].ToObject<DateTime>();
                    ConfigGPIO gpio = (ConfigGPIO)item["configgpio"]["desc"].ToString();
                    Boolean estado = item["configgpio"]["estado"].ToObject<Boolean>();
                    String desc = item["desc"].ToString();
                    HumedadYTemperatura humytemp = null;
                    if (gpio.ToString() == ConfigGPIO.SENSOR_HUM_Y_TEMP.ToString())
                    {
                        JObject jhyt = JObject.Parse(desc);
                        Decimal hum = jhyt["humedad"].ToObject<Decimal>();
                        Decimal temp = jhyt["temperatura"].ToObject<Decimal>();
                        humytemp = new HumedadYTemperatura(hum, temp);
                        resultado.Add(new Evento(fechaYHora, humytemp));
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
                    int id = item["id"].ToObject<int>();
                    Boolean habilitado = item["habilitado"].ToObject<Boolean>();
                    Boolean prender = item["prender"].ToObject<Boolean>();
                    TimeSpan horario1 = item["horario1"].ToObject<TimeSpan>();

                    ConfigGPIO gpio = (ConfigGPIO)item["configgpio"]["desc"].ToString();
                    Boolean estado = item["configgpio"]["estado"].ToObject<Boolean>();
                    String desc = item["desc"].ToString();
                    if (item["horario2"] != null)
                    {
                        TimeSpan horario2 = item["horario1"].ToObject<TimeSpan>();
                        resultado.Add(new Programacion(id, gpio, horario1, horario2, prender, desc, habilitado));
                    }
                    else
                    {
                        resultado.Add(new Programacion(id, gpio, horario1, prender, desc, habilitado));
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
                Decimal humedad = json["humedad"].ToObject<Decimal>();
                Decimal temperatura = json["temperatura"].ToObject<Decimal>();
                resultado = new HumedadYTemperatura(humedad, temperatura);
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            return resultado;
        }

        public static ImagenIndoor parseImagenIndoor(String response)
        {
            ImagenIndoor resultado = null;
            try
            {
                JObject json = JObject.Parse(response);
                bool estado = json["status"].ToObject<Boolean>();
                if (estado)
                {
                    String b64image = json["b64image"].ToString();
                    DateTime fecha = json["date"].ToObject<DateTime>();
                    resultado = new ImagenIndoor(b64image, fecha);
                }
                else
                {
                    String mensaje = json["msg"].ToString();
                    resultado = new ImagenIndoor(mensaje);
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
