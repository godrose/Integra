using Integra.Core.Fake.Contracts;

namespace Integra.Core.Moq
{
    public class MoqFakeProvider : IFakeProvider
    {
        public IFake<TFaked> GetFake<TFaked>() where TFaked : class
        {
            return new MoqFake<TFaked>();
        }
    }
}
