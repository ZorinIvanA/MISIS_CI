using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MISIS.CI.API.Infrastructure.Settings;
using MISIS.CI.Storage.BusinessLogic.Interfaces;
using Newtonsoft.Json;

namespace MISIS.CI.Storage.Infrastructure
{
    public class StorageRepository : IStorageRepository
    {
        private readonly StorageSettings _options;
        public StorageRepository(IOptions<StorageSettings> options)
        {
            _options = options?.Value ??
                       throw new ArgumentNullException(nameof(options));
        }

        public Task SaveAsync(object data, CancellationToken cancellationToken)
        {
            using (var streamWriter = new StreamWriter(_options.FileName))
            {
                return streamWriter.WriteAsync(JsonConvert.SerializeObject(data));
            }
        }
    }
}
