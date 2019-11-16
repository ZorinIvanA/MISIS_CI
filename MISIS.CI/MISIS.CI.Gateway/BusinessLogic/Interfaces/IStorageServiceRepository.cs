using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MISIS.CI.Gateway.BusinessLogic.Dto;

namespace MISIS.CI.Gateway.BusinessLogic.Interfaces
{
    public interface IStorageServiceRepository
    {
        Task SaveAsync(ForecastDto data, CancellationToken cancellationToken);
    }
}
