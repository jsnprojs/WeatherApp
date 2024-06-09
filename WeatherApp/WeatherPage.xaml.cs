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
        await GetWeatherDataByLocation(latitude, longtitude);
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

    private async void TapLocation_Tapped(object sender, TappedEventArgs e)
    {
        await GetCurrentLocation();
        await GetWeatherDataByLocation(latitude, longtitude);
    }

    public async Task GetWeatherDataByLocation(double latitude, double longitude)
    {
        var result = await ApiService.GetWeather(latitude, longtitude);
        UpdateWeatherDataUI(result);
    }

    public async Task GetWeatherDataByCity(string city)
    {
        var result = await ApiService.GetWeatherByCity(city);
        UpdateWeatherDataUI(result);

    }

    private async void ImageButton_Clicked(object sender, EventArgs e)
    {
        var response = await DisplayPromptAsync(
                       title: "", 
                       message: "", 
                       placeholder:"Search weather by city", 
                       accept: "Search", 
                       cancel: "Cancel"
                       );

        if(response != null)
        {
            await GetWeatherDataByCity(response);
        }
    }

    public void UpdateWeatherDataUI(dynamic result)
    {
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
}