using Integra.Core.Fake.Contracts;

namespace Integra.Core.FakeItEasy
{
    public class EasyFakeProvider : IFakeProvider
    {
        public IFake<TFaked> GetFake<TFaked>() where TFaked : class
        {
            return new EasyFake<TFaked>();
        }
    }
}
