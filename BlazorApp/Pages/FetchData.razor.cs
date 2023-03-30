using BlazorApp.Data;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages
{
    public partial class FetchData
    {
        [Inject] WeatherForecastService ForecastService { get; set; }

        private WeatherForecast[]? forecasts;

        protected override async Task OnInitializedAsync()
        {
            forecasts = await ForecastService.GetForecastAsync(DateTime.Now);
        }
    }
}
