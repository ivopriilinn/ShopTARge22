using ShopTARge22.Core.DTO.AccuWeatherDTOs;

namespace ShopTARge22.Core.ServiceInterface
{
    public interface IAccuWeatherForecastsServices
    {
        Task<AccuWeatherLocationResultDTO> AccuWeatherGet(AccuWeatherLocationResultDTO dtoLocation);
        Task<AccuWeatherResultDTO> AccuWeatherResult(AccuWeatherResultDTO dto, AccuWeatherLocationResultDTO dtoLocation);
    }
}
