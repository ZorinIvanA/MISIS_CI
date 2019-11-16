using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MISIS.CI.Gateway.BusinessLogic.Interfaces
{
    public interface IInteractionLogic
    {
        Task SaveForecastAsync(float lat, float lon,
            CancellationToken cancellationToken);
    }
}
