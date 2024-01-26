using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopTARge22.Core.DTO.AccuWeatherDTOs;
using ShopTARge22.Core.ServiceInterface;
using ShopTARge22.Models.AccuWeatherForecasts;

namespace ShopTARge22.Controllers
{
    public class AccuWeatherForecastsController : Controller
    {
        private readonly IAccuWeatherForecastsServices _accuWeatherForecastsServices;

        public AccuWeatherForecastsController 
            (IAccuWeatherForecastsServices accuWeatherForecastsServices) 
        {
            _accuWeatherForecastsServices = accuWeatherForecastsServices;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SearchCity(AccuWeatherSearchCityViewModel vm)
        {
            if (ModelState.IsValid) 
            {
                return RedirectToAction("City", "AccuWeatherForecasts", new { city = vm.City});
            }

            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> City(string city)
        {
            AccuWeatherResultDTO dto = new();
            AccuWeatherLocationResultDTO dtoLocation = new();

            dtoLocation.City = city;

            await _accuWeatherForecastsServices.AccuWeatherResult(dto, dtoLocation);

            AccuWeatherViewModel vm = new();

            vm.City = dtoLocation.City;
            vm.Temperature = dto.Temperature;
            vm.RealFeelTemperature = dto.RealFeelTemperature;
            vm.RelativeHumidity = dto.RelativeHumidity;
            vm.Wind = dto.Wind;
            vm.Pressure = dto.Pressure;
            vm.WeatherText = dto.WeatherText;

            return View(vm);

        }

    }
}
