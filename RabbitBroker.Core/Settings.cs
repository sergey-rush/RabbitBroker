using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace RabbitBroker.Core
{
    public class Settings
    {
        public static string DefaultConnection { get; set; }
        public static string PicturePath { get; set; }
        public IConfiguration Configuration { get; set; }
        public static int MaxPictureSize { get; set; }

        private static Settings _instance;

        public static Settings Instance
        {
            get
            {
                if (_instance == null)
                {
                    throw new NullReferenceException("Settings instance is null");
                }

                return _instance;
            }
        }

        public Settings(IConfiguration configuration)
        {
            Configuration = configuration;
            _instance = this;
        }

        public void Init()
        {
            try
            {
                DefaultConnection = Configuration.GetSection("ConnectionStrings")["DefaultConnection"];
                PicturePath = Configuration.GetSection("Folders")["PicturePath"];

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }
    }
}