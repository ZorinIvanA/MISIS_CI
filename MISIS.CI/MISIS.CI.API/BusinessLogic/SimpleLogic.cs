using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MISIS.CI.API.BusinessLogic.Interfaces;

namespace MISIS.CI.API.BusinessLogic
{
    public class SimpleLogic : ISimpleLogic
    {
        private readonly ISimpleRepository _simpleRepository;

        public SimpleLogic(ISimpleRepository simpleRepository)
        {
            _simpleRepository = simpleRepository ??
                                throw new ArgumentNullException(nameof(simpleRepository));
        }

        public Task<string> GetSimpleForecastAsync(
            float lat, float lon, CancellationToken cancellationToken)
        {
            return _simpleRepository.GetSimpleDataFromAnotherServiceAsync(
                lon, lat, cancellationToken);
        }
    }
}
