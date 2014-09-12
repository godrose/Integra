using System;

namespace Integra.Core.Contracts
{
    public interface IMethodCallMetaData
    {
        string RunMethodDescription { get; }
        Type CallbackType { get; }
    }
}