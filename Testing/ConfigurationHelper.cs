using System;
using System.Collections.Generic;
using System.Text;

namespace Testing
{
    public class ConfigurationHelper
    {
        public ConfigurationHelper()
        {
            //intentionally empty
        }

        public enum HotelAppEnvironment
        {
            LOCAL = 1,
            DOCKER,
            DEV
        }

        public void ResolvePaths(out string baseURL)
        {
            HotelAppEnvironment hotelAppEnvironment = HotelAppEnvironment.DEV;
            baseURL = "";
            switch (hotelAppEnvironment)
            {
                case HotelAppEnvironment.DEV:

                    break;
                case HotelAppEnvironment.DOCKER:

                    break;

                case HotelAppEnvironment.LOCAL:
                    baseURL = "172.0.0.1";
                    break;
            }
        }
    }
}
