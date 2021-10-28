using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherTrackerAPI.Models.Resource;

namespace WeatherTrackerAPI.Services
{
    public class OpenDataProvider : IOpenDataProvider
    {
        private readonly ILogger<OpenDataProvider> _logger;
        private readonly IConfiguration Configuration;

        private RestClient client;
        private string apiKey;

        public OpenDataProvider(ILogger<OpenDataProvider> logger, IConfiguration configuration)
        {
            _logger = logger;
            Configuration = configuration;
            client = new RestClient("https://opendata.cwb.gov.tw/api/v1/rest/datastore/F-C0032-001");
            apiKey = Configuration["OpenDataAPIKey"];
        }


        public WeatherResponseBody Get36HourWeatherForecast()
        {
            var request = new RestRequest("", Method.GET).AddQueryParameter("Authorization", apiKey);
            var response = client.Get<WeatherResponseBody>(request);
            return response.Data;
        }
    }
}
