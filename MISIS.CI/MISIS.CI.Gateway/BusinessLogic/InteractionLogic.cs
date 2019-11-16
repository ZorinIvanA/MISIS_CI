using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MISIS.CI.Gateway.BusinessLogic.Interfaces;

namespace MISIS.CI.Gateway.BusinessLogic
{
    public class InteractionLogic : IInteractionLogic
    {
        private readonly IRemoteApiServiceRepository _remoteApiServiceRepository;
        private readonly IStorageServiceRepository _storageServiceRepository;

        public InteractionLogic(IRemoteApiServiceRepository remoteApiServiceRepository,
            IStorageServiceRepository storageServiceRepository)
        {
            _remoteApiServiceRepository = remoteApiServiceRepository ??
                                          throw new ArgumentNullException(nameof(remoteApiServiceRepository));
            _storageServiceRepository = storageServiceRepository ??
                                        throw new ArgumentNullException(nameof(storageServiceRepository));
        }

        public async Task SaveForecastAsync(float lat, float lon, CancellationToken cancellationToken)
        {
            var forecast = await _remoteApiServiceRepository.GetForecastDataAsync(lat, lon, cancellationToken);
            await _storageServiceRepository.SaveAsync(forecast, cancellationToken);
        }
    }
}
