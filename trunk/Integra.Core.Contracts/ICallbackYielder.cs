namespace Integra.Core.Contracts
{
    public interface ICallbackYielder<out TCallback>
    {
        TCallback YieldCallback();
    }
}