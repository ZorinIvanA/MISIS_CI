using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MISIS.CI.Gateway.BusinessLogic.Dto;
using MISIS.CI.Gateway.BusinessLogic.Interfaces;
using MISIS.CI.Gateway.Infrastructure.Settings;
using Newtonsoft.Json;

namespace MISIS.CI.Gateway.Infrastructure.Repositories
{
    public class RemoteApiRepository : IRemoteApiServiceRepository
    {
        private readonly GatewaySettings _settings;
        public RemoteApiRepository(IOptions<GatewaySettings> settings)
        {
            _settings = settings?.Value ?? throw new ArgumentNullException(nameof(settings));
        }

        public async Task<ForecastDto> GetForecastDataAsync(float lat, float lon, CancellationToken cancellationToken)
        {
            using (HttpClient client = new HttpClient())
            {
                UriBuilder uriBuilder = new UriBuilder(new Uri(_settings.RemoteApiAddress))
                {
                    Path = $"api/simple",
                    Query = $"lat={lat}&lon={lon}"
                };
                var weatherApiResponse = await client.GetAsync(uriBuilder.Uri.ToString(), cancellationToken);
                if (weatherApiResponse != null && weatherApiResponse.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<ForecastDto>(await weatherApiResponse.Content.ReadAsStringAsync());

                throw new InvalidOperationException(weatherApiResponse?.ReasonPhrase ??
                                                    "Get forecast error!");
            }
        }
    }
}
