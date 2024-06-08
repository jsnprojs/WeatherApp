using WeatherApp.Services;

namespace WeatherApp;

public partial class WeatherPage : ContentPage
{
    public List<Models.List> WeatherList;
    private double latitude;
    private double longtitude;
	public WeatherPage()
	{
		InitializeComponent();
        WeatherList = new();
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await GetCurrentLocation();
        var result = await ApiService.GetWeather(latitude, longtitude);

        foreach (var item in result.list)
        {
            WeatherList.Add(item);
        }
        CvWeather.ItemsSource = WeatherList;
        
        LblCity.Text = result.city.name;
        LblWeatherDescription.Text = result.list[0].weather[0].description;

        LblTemperature.Text = result.list[0].main.roundedTemp + "°C";
        LblHumidty.Text = result.list[0].main.humidity + "%";
        LblWind.Text = result.list[0].wind.speed + "km/h";
        ImgWeatherIcon.Source = result.list[0].weather[0].customIcon;
    }

    public async Task GetCurrentLocation()
    {
       Location? location = await Geolocation.GetLocationAsync();
        if(location != null)
        {
            latitude   = location.Latitude;
            longtitude = location.Longitude;
        }
    }
}