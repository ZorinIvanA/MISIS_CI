using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MISIS.CI.API.BusinessLogic.Interfaces;
using MISIS.CI.Storage.BusinessLogic.Interfaces;

namespace MISIS.CI.API.BusinessLogic
{
    public class StorageLogic : IStorageLogic
    {
        private readonly IStorageRepository _simpleRepository;

        public StorageLogic(IStorageRepository simpleRepository)
        {
            _simpleRepository = simpleRepository ??
                                throw new ArgumentNullException(nameof(simpleRepository));
        }

        public Task SaveToFileAsync(object data, CancellationToken cancellationToken)
        {
            return _simpleRepository.SaveAsync(data, cancellationToken);
        }
    }
}
