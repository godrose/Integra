namespace Integra.Core.Contracts
{
    public interface IFakeProvider
    {
        IFake<TFaked> GetFake<TFaked>() where TFaked : class;
    }
}
