namespace Integra.Core.Fake.Contracts
{
    public interface IFakeProvider
    {
        IFake<TFaked> GetFake<TFaked>() where TFaked : class;
    }
}
