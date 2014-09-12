namespace Integra.Core.Contracts
{
    public interface IAppendCallbacks<in TCallback>
    {
        void AppendCallbacks(IHaveCallbacks<TCallback> haveCallbacks);
    }
}