using Microsoft.Extensions.Configuration;
using RocketPlatform.Common;
using Xunit;

namespace RocketPlatform.Test
{
    public class PlatformReservationTests
    {
        private static AppConfig config = new AppConfig();

        public PlatformReservationTests()
        {
            IConfiguration Configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();

            config = Configuration.GetSection("Settings").Get<AppConfig>();
        }

        [Fact]
        public void IsOkForLanding_1()
        {
            //Arrange
            LandingController.lastReservedPoint = new LandingPoint();

            var point = new LandingPoint();
            point.X = 5;
            point.Y = 5;

            //Act 
            var result = LandingController.ReserveLandingPosition(point);

            //Assert 
            Assert.Equal("ok for landing", result);
        }

        [Fact]
        public void IsOkForLanding_2()
        {
            //Arrange
            LandingController.lastReservedPoint = new LandingPoint();

            var point = new LandingPoint();
            point.X = 7;
            point.Y = 7;

            //Act 
            var result = LandingController.ReserveLandingPosition(point);

            //Assert 
            Assert.Equal("ok for landing", result);
        }

        [Fact]
        public void IsPositionsClashed()
        {
            //Arrange
            LandingController.lastReservedPoint = new LandingPoint();

            var point_first_reserv = new LandingPoint();
            point_first_reserv.X = 7;
            point_first_reserv.Y = 7;

            var clashedPoint_1 = new LandingPoint();
            clashedPoint_1.X = 7;
            clashedPoint_1.Y = 8;

            var clashedPoint_2 = new LandingPoint();
            clashedPoint_2.X = 6;
            clashedPoint_2.Y = 7;
            
            var clashedPoint_3 = new LandingPoint();
            clashedPoint_3.X = 6;
            clashedPoint_3.Y = 6;

            //Act 
            var result_first_reserv = LandingController.ReserveLandingPosition(point_first_reserv);
            var result_clashedPoint_1 = LandingController.ReserveLandingPosition(clashedPoint_1);
            var result_clashedPoint_2 = LandingController.ReserveLandingPosition(clashedPoint_2);
            var result_clashedPoint_3 = LandingController.ReserveLandingPosition(clashedPoint_3);

            //Assert 
            Assert.Equal("ok for landing", result_first_reserv);
            Assert.Equal("clash", result_clashedPoint_1);
            Assert.Equal("clash", result_clashedPoint_2);
            Assert.Equal("clash", result_clashedPoint_3);
        }

        [Fact]
        public void IsOutOfPlatform()
        {
            //Arrange
            LandingController.lastReservedPoint = new LandingPoint();

            var point = new LandingPoint();
            point.X = 16;
            point.Y = 15;

            //Act 
            var result = LandingController.ReserveLandingPosition(point);

            //Assert 
            Assert.Equal("out of platform", result);
        }

        [Fact]
        public void IsConfigurationSet()
        {
            var result = ValidationHelper.ValidateConfiguration(config);

            //Assert 
            Assert.True(result);
        }
    }
}
