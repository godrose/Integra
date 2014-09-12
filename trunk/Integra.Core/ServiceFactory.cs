using System.Collections.Generic;
using System.Linq;
using Integra.Core.Contracts;

namespace Integra.Core
{
    class ServiceFactory<TService> : IServiceFactory<TService> where TService : class
    {
        private readonly IFakeProvider _fakeProvider;

        public ServiceFactory(IFakeProvider fakeProvider)
        {
            _fakeProvider = fakeProvider;
        }

        public TService CreateService(IEnumerable<object> methodCalls)
        {
            var fake = _fakeProvider.GetFake<TService>();
            var calls = methodCalls as object[] ?? methodCalls.ToArray();
            VisitMethodCalls(new MethodCallVisitor<TService>(fake) as IMethodCallVisitor<TService>, calls);
            VisitMethodCalls(new MethodCallWithResultVisitor<TService>(fake) as IMethodCallWithResultVisitor<TService>, calls);
            return fake.GetFake();
        }

        private void VisitMethodCalls<TVisitor>(TVisitor visitor, IEnumerable<object> methodCalls)
        {
            foreach (var methodCall in methodCalls.OfType<IAcceptorWithParameters<TVisitor>>())
            {
                methodCall.Accept(visitor);
            }
        }
    }
}
