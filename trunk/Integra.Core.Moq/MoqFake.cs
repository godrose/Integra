using System;
using System.Linq.Expressions;
using Integra.Core.Fake.Contracts;
using Moq;

namespace Integra.Core.Moq
{    
    public class MoqFake<TFaked> : IFake<TFaked> where TFaked : class
    {
        private readonly Mock<TFaked> _fake = new Mock<TFaked>(MockBehavior.Strict);

        public IFakeCallback Setup(Expression<Action<TFaked>> expression)
        {
            return new MoqFakeCallback<TFaked>(_fake.Setup(expression));
        }

        public IFakeCallbackWithResult<TResult> Setup<TResult>(Expression<Func<TFaked, TResult>> expression)
        {
            return new MoqFakeCallbackWithResult<TFaked, TResult>(_fake.Setup(expression));
        }

        public TFaked GetFaked()
        {
            return _fake.Object;
        }
    }
}