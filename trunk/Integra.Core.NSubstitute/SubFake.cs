using System;
using System.Linq.Expressions;
using Integra.Core.Fake.Contracts;
using NSubstitute;

namespace Integra.Core.NSubstitute
{
    public class SubFake<TFaked> : IFake<TFaked> where TFaked : class
    {
        private readonly TFaked _faked = Substitute.For<TFaked>();

        public IFakeCallback Setup(Expression<Action<TFaked>> expression)
        {
            return new SubFakeCallback<TFaked>(_faked.When(expression.Compile()));
        }

        public IFakeCallbackWithResult<TResult> Setup<TResult>(Expression<Func<TFaked, TResult>> expression)
        {
            return new SubFakeCallbackWithResult<TFaked,TResult>(_faked);
        }

        public TFaked GetFaked()
        {
            return _faked;
        }
    }
}
