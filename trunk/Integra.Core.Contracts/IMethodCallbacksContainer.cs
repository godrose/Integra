namespace Integra.Core.Contracts
{
    public interface IMethodCallbacksContainer<TCallback> : IHaveCallbacks<TCallback>, IAppendCallbacks<TCallback>, ICallbackYielder<TCallback>, IMethodCallMetaData
    {
    }
}