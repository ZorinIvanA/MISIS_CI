using System.Threading;
using System.Threading.Tasks;

namespace MISIS.CI.Storage.BusinessLogic.Interfaces
{
    public interface IStorageRepository
    {
        Task SaveAsync(object data,
             CancellationToken cancellationToken);
    }
}
