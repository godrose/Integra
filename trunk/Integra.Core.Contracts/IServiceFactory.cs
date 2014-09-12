using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Integra.Core.Contracts
{
    public interface IServiceFactory<out TService>
    {
        TService CreateService(IEnumerable<object> methodCalls);
    }

    public interface IFake<TFake>
    {
        IFakeCallback Setup(Expression<Action<TFake>> expression);
        IFakeCallbackWithResult<TResult> Setup<TResult>(Expression<Func<TFake, TResult>> expression);
        TFake GetFake();
    }

    public interface IFakeCallback
    {
        void Callback(Action action);
        void Callback<T>(Action<T> action);
        void Callback<T1, T2>(Action<T1, T2> action);
        void Callback<T1, T2, T3>(Action<T1, T2, T3> action);
        void Callback<T1, T2, T3, T4>(Action<T1, T2, T3, T4> action);
        void Callback<T1, T2, T3, T4, T5>(Action<T1, T2, T3, T4, T5> action);
    }

    public interface IFakeCallbackWithResult<in TResult>
    {
        void Callback(Func<TResult> func);
    }
}