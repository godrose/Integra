namespace Integra.Core.Contracts
{
    public interface IServiceCall<TService> : IHaveMethods<TService>, IAppendMethods<TService> where TService : class
    {
        TService GetService();
    }
}