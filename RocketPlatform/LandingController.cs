using Microsoft.Extensions.Configuration;
using RocketPlatform.Common;

namespace RocketPlatform
{
    public class LandingController
    {
        public static LandingPoint lastReservedPoint = new LandingPoint();

        private static AppConfig config = new AppConfig();

        static LandingController()
        {
            IConfiguration Configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();

            config = Configuration.GetSection("Settings").Get<AppConfig>();
        }

        public static string ReserveLandingPosition(LandingPoint point)
        {
            if(!ValidationHelper.ValidateConfiguration(config))
            {
                return "wrong configuration";
            }

            if(!ValidationHelper.ValidateTrajectory(point, config))
            {
                return "out of platform";
            }

            if (!ValidationHelper.ValidateClash(point, lastReservedPoint))
            {
                return "clash";
            }

            lastReservedPoint.X = point.X;
            lastReservedPoint.Y = point.Y;


            return "ok for landing";
        }
    }
}