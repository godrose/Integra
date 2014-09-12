namespace Integra.Core.Contracts
{
    public interface IMethodCallbackTemplateVisitor
    {
        IMethodCallback Visit(MethodCallbackTemplate methodCallbackTemplate);
        IMethodCallback<T> Visit<T>(MethodCallbackTemplate<T> methodCallbackTemplate);
        IMethodCallback<T1, T2> Visit<T1, T2>(MethodCallbackTemplate<T1, T2> methodCallbackTemplate);
    }

    public class MethodCallbackTemplate : IAcceptorWithParametersResult<IMethodCallbackTemplateVisitor, IMethodCallback>
    {
        public IMethodCallback Accept(IMethodCallbackTemplateVisitor visitor)
        {
            return visitor.Visit(this);
        }

        public MethodCallbackTemplate SetActionWrapper(IActionWrapper actionWrapper)
        {
            ActionWrapper = actionWrapper;
            return this;
        }

        public IActionWrapper ActionWrapper { get; private set; }
    }

    public class MethodCallbackTemplate<T> : IAcceptorWithParametersResult<IMethodCallbackTemplateVisitor, IMethodCallback<T>>
    {
        public IMethodCallback<T> Accept(IMethodCallbackTemplateVisitor visitor)
        {
            return visitor.Visit(this);
        }

        public MethodCallbackTemplate<T> SetActionWrapper(IActionWrapper<T> actionWrapper)
        {
            ActionWrapper = actionWrapper;
            return this;
        }

        public IActionWrapper<T> ActionWrapper { get; private set; }
    }

    public class MethodCallbackTemplate<T1, T2> : IAcceptorWithParametersResult<IMethodCallbackTemplateVisitor, IMethodCallback<T1, T2>>
    {
        public IMethodCallback<T1, T2> Accept(IMethodCallbackTemplateVisitor visitor)
        {
            return visitor.Visit(this);
        }

        public MethodCallbackTemplate<T1, T2> SetActionWrapper(IActionWrapper<T1, T2> actionWrapper)
        {
            ActionWrapper = actionWrapper;
            return this;
        }

        public IActionWrapper<T1, T2> ActionWrapper { get; private set; }
    }

    internal class OnCompleteCallbackVisitor : IMethodCallbackTemplateVisitor
    {
        public IMethodCallback Visit(MethodCallbackTemplate methodCallbackTemplate)
        {
            return new OnCompleteCallback(methodCallbackTemplate.ActionWrapper.Action);
        }

        public IMethodCallback<T> Visit<T>(MethodCallbackTemplate<T> methodCallbackTemplate)
        {
            return new OnCompleteCallback<T>(methodCallbackTemplate.ActionWrapper.Action);
        }

        public IMethodCallback<T1, T2> Visit<T1, T2>(MethodCallbackTemplate<T1, T2> methodCallbackTemplate)
        {
            return new OnCompleteCallback<T1, T2>(methodCallbackTemplate.ActionWrapper.Action);
        }
    }
}