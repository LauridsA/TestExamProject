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
            basePathAPI = "";
            HotelAppEnvironment hotelAppEnvironment = HotelAppEnvironment.DOCKER; //fallback
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            connectionString = "";
            switch (hotelAppEnvironment)
            {
                case HotelAppEnvironment.DEV:
                    connectionString = "DEVELOPMENT BASEPATH";
                    break;
                case HotelAppEnvironment.DOCKER:
                    connectionString = @"server=172.17.0.2,1433;Initial catalog=HotelBooking;User=sa;Password=360@NoScopes!;MultipleActiveResultSets=True;";
                    break;

                case HotelAppEnvironment.LOCAL:
                    connectionString = "172.0.0.1";
                    break;
            }
        }
    }
}
