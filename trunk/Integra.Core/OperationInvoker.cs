using Integra.Core.Contracts;

namespace Integra.Core
{
    class OperationInvoker
    {
    }

    internal static class OperationStarter
    {
        internal static void StartOperation(IOperation operation)
        {
            operation.Start();
        }
    }
}
