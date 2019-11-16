using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.Options;
using Microsoft.Extensions.Options;
using MISIS.CI.Gateway.BusinessLogic.Dto;
using MISIS.CI.Gateway.BusinessLogic.Interfaces;
using MISIS.CI.Gateway.Infrastructure.Settings;

namespace MISIS.CI.Gateway.Infrastructure.Repositories
{
    public class StorageServiceRepository : IStorageServiceRepository
    {
        private readonly GatewaySettings _settings;
        public StorageServiceRepository(IOptions<GatewaySettings> settings)
        {
            _settings = settings?.Value ?? throw new ArgumentNullException(nameof(settings));
        }

        public async Task SaveAsync(ForecastDto data, CancellationToken cancellationToken)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            using (HttpClient client = new HttpClient())
            {
                UriBuilder uriBuilder = new UriBuilder(new Uri(_settings.StorageServiceAddress))
                {
                    Path = $"api/storage/save"
                };
                await client.PostAsJsonAsync(
                    uriBuilder.Uri.ToString(), data, cancellationToken);
            }
        }
    }
}
