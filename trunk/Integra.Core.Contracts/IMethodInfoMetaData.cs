using System;

namespace Integra.Core.Contracts
{
    public interface IMethodInfoMetaData
    {
        string RunMethodDescription { get; }
        Type CallbackType { get; }
        bool HasDefaultCallback { get; }
    }
}