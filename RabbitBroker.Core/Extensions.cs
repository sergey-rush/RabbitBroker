using System;
using Microsoft.Extensions.Configuration;

namespace RabbitBroker.Core
{
    public static class Extensions
    {
        public static IConfiguration ConfigureSettings(this IConfiguration configuration)
        {
            Settings settings = new Settings(configuration);
            settings.Init();
            return configuration;
        }

        public static double ToDouble(this string input)
        {
            double result = Convert.ToDouble(input);
            return result;
        }

        public static int ToInt32(this string input)
        {
            int result = Convert.ToInt32(input);
            return result;
        }

        public static string ToStringPrice(this decimal value)
        {
            string output = value.ToString("C", new System.Globalization.CultureInfo("ru-RU"));
            return output;
        }

        public static string ToImagePath(this string url)
        {
            string output = string.Format("/{0}/{1}", Settings.PicturePath, url);
            return output;
        }
    }
}