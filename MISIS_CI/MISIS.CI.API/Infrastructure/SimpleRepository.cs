using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Options;
using MISIS.CI.API.BusinessLogic.Interfaces;
using MISIS.CI.API.Infrastructure.Settings;

namespace MISIS.CI.API.Infrastructure
{
    public class SimpleRepository : ISimpleRepository
    {
        private WeatherAPISettings _options;
        public SimpleRepository(IOptions<WeatherAPISettings> options)
        {
            _options = options?.Value ??
                       throw new ArgumentNullException(nameof(options));
        }

        public async Task<string> GetSimpleDataFromAnotherServiceAsync(
            float longitude, float latitude, CancellationToken cancellationToken)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("x-rapidapi-host", "dark-sky.p.rapidapi.com");
                client.DefaultRequestHeaders.Add("x-rapidapi-key", _options.SecretKey);
                UriBuilder uriBuilder = new UriBuilder(new Uri(_options.ApiUri))
                {
                    Path = $"{latitude.ToString("G").Replace(',','.')},{longitude.ToString("G").Replace(',', '.')}",
                    Query = $"lang={_options.Language}&units=auto"
                };
                var weatherApiResponse = await client.GetAsync(uriBuilder.Uri.ToString(), cancellationToken);
                if (weatherApiResponse != null && weatherApiResponse.IsSuccessStatusCode)
                    return await weatherApiResponse.Content.ReadAsStringAsync();

                throw new InvalidOperationException(weatherApiResponse?.ReasonPhrase ??
                                                    "Get forecast error!");
            }
        }
    }
}
