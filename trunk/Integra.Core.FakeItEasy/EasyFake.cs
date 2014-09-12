using System;
using System.Linq.Expressions;
using FakeItEasy;
using Integra.Core.Fake.Contracts;

namespace Integra.Core.FakeItEasy
{
    public class EasyFake<TFaked> : IFake<TFaked>
    {
        private readonly TFaked _faked = A.Fake<TFaked>();

        public IFakeCallback Setup(Expression<Action<TFaked>> expression)
        {
            return new EasyFakeCallback<TFaked>(A.CallTo(expression));
        }

        public IFakeCallbackWithResult<TResult> Setup<TResult>(Expression<Func<TFaked, TResult>> expression)
        {
            throw new NotImplementedException();
        }

        public TFaked GetFaked()
        {
            return _faked;
        }
    }
}
