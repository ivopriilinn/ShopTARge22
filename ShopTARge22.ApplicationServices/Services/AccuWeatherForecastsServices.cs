using Nancy.Json;
using ShopTARge22.Core.DTO.AccuWeatherDTOs;
using ShopTARge22.Core.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ShopTARge22.ApplicationServices.Services
{
    public class AccuWeatherForecastsServices : IAccuWeatherForecastsServices
    {
        readonly string API_Key = "13GzXUQSqaeIHRNog0dewX4XIy0qsP1N";


        public async Task<AccuWeatherLocationResultDTO> AccuWeatherGet(AccuWeatherLocationResultDTO dtoLocation)
        {

            try
            {
                var url1 = $"http://dataservice.accuweather.com/locations/v1/cities/search?apikey={API_Key}&q={dtoLocation.City}";

                using (WebClient client = new WebClient())
                {
                    string json = client.DownloadString(url1);
                    List<AccuWeatherLocationRootDTO> accuGet = new JavaScriptSerializer().Deserialize<List<AccuWeatherLocationRootDTO>>(json);


                        dtoLocation.Key = accuGet[0].Key;
                        dtoLocation.Country = accuGet[0].Country;
                        dtoLocation.LocalizedName = accuGet[0].LocalizedName;
       

                }
            }
            catch (NullReferenceException nullEx)
            {
                Console.WriteLine($"Null reference exception: {nullEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }

            return dtoLocation;
        }


        public async Task<AccuWeatherResultDTO> AccuWeatherResult(AccuWeatherResultDTO dto, AccuWeatherLocationResultDTO dtoLocation)
        {
            await AccuWeatherGet(dtoLocation);

            if (!string.IsNullOrEmpty(dtoLocation.Key))
            {
                string url = $"http://dataservice.accuweather.com/currentconditions/v1/{dtoLocation.Key}?apikey={API_Key}&details=true";

                using (WebClient client = new WebClient())
                {
                    string json = client.DownloadString(url);
                    List<AccuWeatherRootDTO> accuResult = new JavaScriptSerializer().Deserialize<List<AccuWeatherRootDTO>>(json);
                        
                        dto.Temperature = accuResult[0].Temperature.Metric.Value;
                        dto.RealFeelTemperature = accuResult[0].RealFeelTemperature.Metric.Value;
                        dto.RelativeHumidity = accuResult[0].RelativeHumidity;
                        dto.Wind = accuResult[0].Wind.Speed.Metric.Value;
                        dto.Pressure = accuResult[0].Pressure.Metric.Value;
                        dto.WeatherText = accuResult[0].WeatherText;
                }
            }

            return dto;
        }
    }
}
