using System;

namespace Integra.Core.Fake.Contracts
{
    public interface IFakeCallbackWithResult<in TResult>
    {
        void Callback(Func<TResult> func);
    }
}