using System.Collections.Generic;

namespace Integra.Core.Contracts
{
    public interface IServiceFactory<out TService>
    {
        TService CreateService(IEnumerable<object> methodCalls);
    }
}