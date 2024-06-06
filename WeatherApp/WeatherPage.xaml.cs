using WeatherApp.Services;

namespace WeatherApp;

public partial class WeatherPage : ContentPage
{
	public WeatherPage()
	{
		InitializeComponent();

    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();

        var result = await ApiService.GetWeatherByCity("Elk Grove");

        LblCity.Text = result.city.name;
        LblWeatherDescription.Text = result.list[0].weather[0].description;

        LblTemperature.Text = result.list[0].main.temp + "°C";
    }
}