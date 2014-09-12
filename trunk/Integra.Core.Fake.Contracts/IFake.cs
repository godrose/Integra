using System;
using System.Linq.Expressions;

namespace Integra.Core.Fake.Contracts
{
    public interface IFake<TFake>
    {
        IFakeCallback Setup(Expression<Action<TFake>> expression);
        IFakeCallbackWithResult<TResult> Setup<TResult>(Expression<Func<TFake, TResult>> expression);
        TFake GetFaked();
    }
}