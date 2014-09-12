using System;
using Integra.Core.Fake.Contracts;
using NSubstitute;
using NSubstitute.Core;

namespace Integra.Core.NSubstitute
{
    public class SubFakeCallback<TFaked> : IFakeCallback
    {
        private readonly WhenCalled<TFaked> _whenCalled;

        public SubFakeCallback(WhenCalled<TFaked> whenCalled)
        {
            _whenCalled = whenCalled;
        }

        public void Callback(Action action)
        {
            _whenCalled.Do(r => action());
        }

        public void Callback<T>(Action<T> action)
        {
            _whenCalled.Do(r => action(Arg.Any<T>()));
        }

        public void Callback<T1, T2>(Action<T1, T2> action)
        {
            _whenCalled.Do(r => action(Arg.Any<T1>(), Arg.Any<T2>()));
        }

        public void Callback<T1, T2, T3>(Action<T1, T2, T3> action)
        {
            _whenCalled.Do(r => action(Arg.Any<T1>(), Arg.Any<T2>(), Arg.Any<T3>()));
        }

        public void Callback<T1, T2, T3, T4>(Action<T1, T2, T3, T4> action)
        {
            throw new NotImplementedException();
        }

        public void Callback<T1, T2, T3, T4, T5>(Action<T1, T2, T3, T4, T5> action)
        {
            throw new NotImplementedException();
        }
    }

    public class SubFakeCallbackWithResult<TFaked, TResult> : IFakeCallbackWithResult<TResult>
    {
        private readonly TFaked _faked;

        public SubFakeCallbackWithResult(TFaked faked)
        {
            _faked = faked;
        }

        public void Callback(Func<TResult> func)
        {
            throw new NotImplementedException("Nsubstitute relies on Thread local storage and does not allow for faking funcs by specifying their expression. In order to support custom callbacks for funcs a TLS-related development is needed");
        }
    }
}
