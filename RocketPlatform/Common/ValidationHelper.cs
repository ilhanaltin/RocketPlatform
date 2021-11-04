
namespace RocketPlatform.Common
{
    public class ValidationHelper
    {
        public static bool ValidateTrajectory(LandingPoint point, AppConfig config)
        {
            if (point.X < config.PlatformSettings.X || point.X > config.PlatformSettings.X + config.PlatformSettings.Size
               || point.Y < config.PlatformSettings.Y || point.Y > config.PlatformSettings.Y + config.PlatformSettings.Size)
            {
                return false;
            }

            return true;
        }

        public static bool ValidateClash(LandingPoint point, LandingPoint lastReservedPoint)
        {
            if (lastReservedPoint.X > 0 && lastReservedPoint.Y > 0)
            {
                if (point.X == lastReservedPoint.X && point.Y == lastReservedPoint.Y)
                {
                    return false;
                }

                if (point.X >= lastReservedPoint.X - 1 || point.X <= lastReservedPoint.X + 1
                    || point.Y >= lastReservedPoint.Y - 1 || point.Y <= lastReservedPoint.Y + 1)
                {
                    return false;
                }
            }

            return true;
        }

        public static bool ValidateConfiguration(AppConfig config)
        {
            if (config.PlatformSettings.Size > config.LandingAreaSettings.Size)
            {
                return false;
            }

            if (config.PlatformSettings.X + config.PlatformSettings.Size > config.LandingAreaSettings.X + config.LandingAreaSettings.Size
                || config.PlatformSettings.Y + config.PlatformSettings.Size > config.LandingAreaSettings.Y + config.LandingAreaSettings.Size)
            {
                return false;
            }

            return true;
        }
    }
}
