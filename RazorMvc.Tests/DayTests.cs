using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Abstractions;
using RazorMvc.Utilities;
using RazorMvc.WebAPI;
using RazorMvc.WebAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RazorMvc.Tests
{
    public class DayTests
    {
        private IConfigurationRoot configuration;

        public DayTests()
        {
            configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        }

        [Fact]
        public void CheckEpochConversion()
        {
            // Assume
            long ticks = 1617184800;

            // Act
            DateTime dateTime = DateTimeConverter.ConvertEpochToDateTime(ticks);

            // Assert
            Assert.Equal(31, dateTime.Day);
            Assert.Equal(3, dateTime.Month);
            Assert.Equal(2021, dateTime.Year);
        }

        [Fact]
        public void ConvertOutputOfWeatherAPIToWeatherForecast()
        {
            // Assume
            //https://api.openweathermap.org/data/2.5/onecall?lat=45.75&lon=25.3333&exclude=hourly,minutely&appid=c8faf2bb86a7652b7567d6de3e7dbfa1
            WeatherForecastController weatherForecastController = InstantiateWeatherForecastController();

            // Act
            var weatherForecasts = weatherForecastController.FetchWeatherForecasts();

            // Assert
            Assert.Equal(8, weatherForecasts.Count);
        }

        [Fact]
        public void ConvertWeatherJsonToWeatherForecast()
        {
            // Assume
            string content = @"{""lat"":45.75,""lon"":25.3333,""timezone"":""Europe / Bucharest"",""timezone_offset"":10800,""current"":{""dt"":1617178074,""sunrise"":1617163233,""sunset"":1617209087,""temp"":282.8,""feels_like"":280.23,""pressure"":1023,""humidity"":52,""dew_point"":273.43,""uvi"":2.71,""clouds"":27,""visibility"":10000,""wind_speed"":0.89,""wind_deg"":67,""wind_gust"":0.89,""weather"":[{""id"":802,""main"":""Clouds"",""description"":""scattered clouds"",""icon"":""03d""}]},""daily"":[{""dt"":1617184800,""sunrise"":1617163233,""sunset"":1617209087,""temp"":{""day"":284.24,""min"":274.45,""max"":285.52,""night"":281.2,""eve"":282.84,""morn"":274.45},""feels_like"":{""day"":281.3,""night"":279.76,""eve"":281.1,""morn"":271.64},""pressure"":1026,""humidity"":51,""dew_point"":274.5,""wind_speed"":1.66,""wind_deg"":335,""weather"":[{""id"":802,""main"":""Clouds"",""description"":""scattered clouds"",""icon"":""03d""}],""clouds"":37,""pop"":0,""uvi"":4.05},{""dt"":1617271200,""sunrise"":1617249518,""sunset"":1617295565,""temp"":{""day"":285.39,""min"":279.72,""max"":288.46,""night"":282.02,""eve"":285.5,""morn"":279.72},""feels_like"":{""day"":283.53,""night"":279.37,""eve"":283.51,""morn"":278.05},""pressure"":1020,""humidity"":61,""dew_point"":278.1,""wind_speed"":1.04,""wind_deg"":65,""weather"":[{""id"":804,""main"":""Clouds"",""description"":""overcast clouds"",""icon"":""04d""}],""clouds"":100,""pop"":0.04,""uvi"":4.39},{""dt"":1617357600,""sunrise"":1617335804,""sunset"":1617382043,""temp"":{""day"":288.08,""min"":280.32,""max"":288.66,""night"":283.05,""eve"":285.8,""morn"":280.55},""feels_like"":{""day"":285.3,""night"":280.8,""eve"":282.95,""morn"":278.6},""pressure"":1010,""humidity"":60,""dew_point"":280.27,""wind_speed"":3.05,""wind_deg"":298,""weather"":[{""id"":500,""main"":""Rain"",""description"":""light rain"",""icon"":""10d""}],""clouds"":100,""pop"":1,""rain"":3.44,""uvi"":2.68},{""dt"":1617444000,""sunrise"":1617422090,""sunset"":1617468520,""temp"":{""day"":285.33,""min"":278.05,""max"":285.33,""night"":278.05,""eve"":281.45,""morn"":281.65},""feels_like"":{""day"":282.2,""night"":274.13,""eve"":277.25,""morn"":278},""pressure"":1006,""humidity"":58,""dew_point"":277.32,""wind_speed"":2.63,""wind_deg"":298,""weather"":[{""id"":501,""main"":""Rain"",""description"":""moderate rain"",""icon"":""10d""}],""clouds"":29,""pop"":1,""rain"":8.29,""uvi"":3.49},{""dt"":1617530400,""sunrise"":1617508376,""sunset"":1617554998,""temp"":{""day"":277.74,""min"":275.27,""max"":277.74,""night"":275.27,""eve"":275.59,""morn"":277.17},""feels_like"":{""day"":274.74,""night"":272.07,""eve"":272.45,""morn"":275.18},""pressure"":1012,""humidity"":93,""dew_point"":276.59,""wind_speed"":2.28,""wind_deg"":82,""weather"":[{""id"":616,""main"":""Snow"",""description"":""rain and snow"",""icon"":""13d""}],""clouds"":100,""pop"":1,""rain"":6.33,""snow"":1.42,""uvi"":1.6},{""dt"":1617616800,""sunrise"":1617594662,""sunset"":1617641476,""temp"":{""day"":274.95,""min"":274.22,""max"":275.42,""night"":275.42,""eve"":274.92,""morn"":274.54},""feels_like"":{""day"":271.16,""night"":269.37,""eve"":269.15,""morn"":272.04},""pressure"":1004,""humidity"":99,""dew_point"":274.7,""wind_speed"":2.95,""wind_deg"":283,""weather"":[{""id"":616,""main"":""Snow"",""description"":""rain and snow"",""icon"":""13d""}],""clouds"":100,""pop"":1,""rain"":1.9,""snow"":12.21,""uvi"":2},{""dt"":1617703200,""sunrise"":1617680949,""sunset"":1617727954,""temp"":{""day"":281.98,""min"":273.97,""max"":283.76,""night"":278.43,""eve"":283.66,""morn"":273.97},""feels_like"":{""day"":276.6,""night"":275.55,""eve"":280,""morn"":268.96},""pressure"":1011,""humidity"":66,""dew_point"":275.93,""wind_speed"":5.5,""wind_deg"":288,""weather"":[{""id"":800,""main"":""Clear"",""description"":""clear sky"",""icon"":""01d""}],""clouds"":7,""pop"":0.4,""uvi"":2},{""dt"":1617789600,""sunrise"":1617767237,""sunset"":1617814432,""temp"":{""day"":287.02,""min"":275.02,""max"":290.76,""night"":282.25,""eve"":289.38,""morn"":275.02},""feels_like"":{""day"":284.42,""night"":279.59,""eve"":287.26,""morn"":272.04},""pressure"":1015,""humidity"":51,""dew_point"":277.04,""wind_speed"":1.81,""wind_deg"":101,""weather"":[{""id"":800,""main"":""Clear"",""description"":""clear sky"",""icon"":""01d""}],""clouds"":0,""pop"":0,""uvi"":2}]}";
            WeatherForecastController weatherForecastController = InstantiateWeatherForecastController();

            // Act
            var weatherForecasts = weatherForecastController.ConvertResponseContentToWeatherForecastList(content);
            WeatherForecast weatherForecastForTommorow = weatherForecasts[1];

            // Assert
            Assert.Equal(285.39, weatherForecastForTommorow.TemperatureK);
        }

        private WeatherForecastController InstantiateWeatherForecastController()
        {
            Microsoft.Extensions.Logging.ILogger<WeatherForecastController> nullLogger = new NullLogger<WeatherForecastController>();
            var weatherForecastController = new WeatherForecastController(nullLogger, configuration);
            return weatherForecastController;
        }
    }
}