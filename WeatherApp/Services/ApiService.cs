﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Models;


namespace WeatherApp.Services;

public class ApiService
{
    public async Task<Root> GetWeather(double latitude, double longitude)
    {
        string apiUrl = string.Format("http://api.openweathermap.org/data/2.5/forecast?lat={0}&lon={1}&appid=b1a40fac2d8a3e9484e57de36c664fd0", latitude, longitude);
        
        HttpClient httpClient = new();        
        string response = await httpClient.GetStringAsync(apiUrl);

        return JsonConvert.DeserializeObject<Root>(response);
    }
}
