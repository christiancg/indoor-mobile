using System;
namespace indoor.Models
{
    public sealed class ConfigGPIO
    {
        private readonly String name;
        private readonly String value;

        public static readonly ConfigGPIO LUZ = new ConfigGPIO("LUZ", "luz");
        public static readonly ConfigGPIO FAN_INTRA = new ConfigGPIO("FAN_INTRA", "fanintra");
        public static readonly ConfigGPIO FAN_EXTRA = new ConfigGPIO("FAN_EXTRA", "fanextra");
        public static readonly ConfigGPIO BOMBA = new ConfigGPIO("BOMBA", "bomba");
        public static readonly ConfigGPIO SENSOR_HUM_Y_TEMP = new ConfigGPIO("SENSOR_HUM_Y_TEMPß", "humytemp");

        private ConfigGPIO(String value, String name)
        {
            this.name = name;
            this.value = value;
        }

        public override String ToString()
        {
            return name;
        }

        public static explicit operator ConfigGPIO(string str)
        {
            switch (str)
            {
                case "luz":
                    return LUZ;
                case "fanintra":
                    return FAN_INTRA;
                case "fanextra":
                    return FAN_EXTRA;
                case "bomba":
                    return BOMBA;
                case "humytemp":
                    return SENSOR_HUM_Y_TEMP;
                default:
                    throw new InvalidCastException();
            }
        }
    }
}
