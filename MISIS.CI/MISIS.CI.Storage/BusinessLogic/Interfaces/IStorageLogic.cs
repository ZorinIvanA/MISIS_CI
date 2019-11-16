using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MISIS.CI.API.BusinessLogic.Interfaces
{
    public interface IStorageLogic
    {
        Task SaveToFileAsync(object data, CancellationToken cancellationToken);
    }
}
