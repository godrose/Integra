using Integra.Core.Fake.Contracts;

namespace Integra.Core.NSubstitute
{
    class SubFakeProvider : IFakeProvider
    {
        public IFake<TFaked> GetFake<TFaked>() where TFaked : class
        {
            return new SubFake<TFaked>();
        }
    }
}
