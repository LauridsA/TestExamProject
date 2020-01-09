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

        public void ResolvePaths(out string connectionString, out string basePathAPI)
        {
            HotelAppEnvironment hotelAppEnvironment; 
            var env = Environment.GetEnvironmentVariable("ASPNETENVIRONMENT");
            if (env == "Docker")
            {
                hotelAppEnvironment = HotelAppEnvironment.DOCKER;
            }
            else if (env == "Development")
            {
                hotelAppEnvironment = HotelAppEnvironment.DEV;
            }
            else
            {
                hotelAppEnvironment = HotelAppEnvironment.LOCAL; //fallback
            }

            basePathAPI = "";
            connectionString = "";
            switch (hotelAppEnvironment)
            {
                case HotelAppEnvironment.DEV:
                    connectionString = "DEVELOPMENT BASEPATH";
                    break;
                case HotelAppEnvironment.DOCKER:
                    connectionString = @"Server=172.17.0.2,1433;Initial catalog=HotelBooking;User=sa;Password=360@NoScopes!;MultipleActiveResultSets=True;";
                    basePathAPI = "https://172.17.0.3:8000/";
                    break;
                case HotelAppEnvironment.LOCAL:
                    connectionString = @"Server=.;Initial catalog=HotelBooking;Integrated Security=true;MultipleActiveResultSets=True;";
                    basePathAPI = "https://localhost:44357/";
                    break;
            }
        }
    }
}
