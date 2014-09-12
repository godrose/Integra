using Integra.Core.Contracts;

namespace Integra.Core
{
    public class MethodCallWithResultVisitor<TService> : IMethodCallWithResultVisitor<TService> where TService : class
    {
        private readonly IFake<TService> _stubService;

        public MethodCallWithResultVisitor(IFake<TService> stubService)
        {
            _stubService = stubService;
        }

        public void Visit<TResult>(IMethodCallWithResult<TService, IMethodCallbackWithResult<TResult>, TResult> methodCall)
        {
            VisitImpl(methodCall);
        }        

        public void Visit<T, TResult>(IMethodCallWithResult<TService, IMethodCallbackWithResult<T, TResult>, TResult> methodCall)
        {
            VisitImpl(methodCall);
        }

        public void Visit<T1, T2, TResult>(IMethodCallWithResult<TService, IMethodCallbackWithResult<T1, T2, TResult>, TResult> methodCall)
        {
            VisitImpl(methodCall);
        }

        private void VisitImpl<TCallback, TResult>(IMethodCallWithResult<TService, TCallback, TResult> methodCall) where TCallback : IAcceptorWithParametersResult<IMethodCallbackWithResultVisitor<TResult>, TResult>
        {
            var methodCallbackWithResultVisitor = new MethodCallbackWithResultVisitor<TResult>();

            _stubService.Setup(methodCall.RunMethod).Callback(() =>
            {
                var methodCallback = methodCall.YieldCallback();
                return methodCallback.Accept(methodCallbackWithResultVisitor);
            });
        }
    }

    public class MethodCallWithResultAppendCallsVisitor<TService> : IMethodCallWithResultVisitor<TService> where TService : class
    {
        private object _newMethodCall;

        public MethodCallWithResultAppendCallsVisitor(object newMethodCall)
        {
            _newMethodCall = newMethodCall;
        }

        public void Visit<TResult>(IMethodCallWithResult<TService, IMethodCallbackWithResult<TResult>, TResult> methodCall)
        {
            AppendCallsVisitorHelper.VisitMethodCall(methodCall, _newMethodCall);
        }

        public void Visit<T, TResult>(IMethodCallWithResult<TService, IMethodCallbackWithResult<T, TResult>, TResult> methodCall)
        {
            AppendCallsVisitorHelper.VisitMethodCall(methodCall, _newMethodCall);
        }

        public void Visit<T1, T2, TResult>(IMethodCallWithResult<TService, IMethodCallbackWithResult<T1, T2, TResult>, TResult> methodCall)
        {
            AppendCallsVisitorHelper.VisitMethodCall(methodCall, _newMethodCall);
        }
    }
}