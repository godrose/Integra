using System;
using System.Collections.Generic;
using System.Linq;
using Integra.Core.Contracts;
using Integra.Core.Moq;

namespace Integra.Core
{      
    public class ServiceCall<TService> : IServiceCall<TService>, IHaveNoMethods<TService> where TService : class
    {
        private readonly IServiceFactory<TService> _serviceFactory = new ServiceFactory<TService>(new MoqFakeProvider());

        private ServiceCall()
        {
            
        }

        private readonly List<IMethodCallMetaData> _methodCalls = new List<IMethodCallMetaData>();
        public IEnumerable<IMethodCallMetaData> MethodCalls 
        {
            get { return _methodCalls; }
        }

        public static IHaveNoMethods<TService> CreateServiceCall()
        {
            return new ServiceCall<TService>();
        }

        private void AddMethodCall(IMethodCallMetaData methodCallMetaData)
        {                        
            var newMethodInfo = methodCallMetaData as IAcceptorWithParameters<IMethodCallVisitor<TService>>;
            if (newMethodInfo != null)
            {
                AddMethodCallImpl(methodCallMetaData, newMethodInfo);
            }
            else
            {
                var newMethodWithResultInfo = methodCallMetaData as IAcceptorWithParameters<IMethodCallWithResultVisitor<TService>>;
                if (newMethodWithResultInfo == null)
                {
                    throw new ArgumentException(
                        "Only method calls that implement acceptor for either MethodInfo or MethodInfoWithResult visitors are allowed",
                        "methodCallMetaData");
                }
                AddMethodCallWithResultImpl(methodCallMetaData, newMethodWithResultInfo);
            }
            return;
        }        

        public IServiceCall<TService> AddMethodCall<TCallback>(IMethodCall<TService,TCallback> methodCall)
        {
            AddMethodCallImpl(methodCall, methodCall);   
            return this;
        }

        public IServiceCall<TService> AddMethodCall<TCallback, TResult>(IMethodCallWithResult<TService, TCallback, TResult> methodCall)
        {
            AddMethodCallWithResultImpl(methodCall, methodCall);
            return this;
        }

        private void AddMethodCallImpl(IMethodCallMetaData methodCallMetaData, IAcceptorWithParameters<IMethodCallVisitor<TService>> acceptorWithParameters)
        {
            var existingMethodCallMetaData = FindExistingMethodCallMetaData(methodCallMetaData);
            if (existingMethodCallMetaData == null)
            {
                _methodCalls.Add(methodCallMetaData);
            }
            else
            {
                AssertCallbackType(methodCallMetaData, existingMethodCallMetaData);                
                AcceptExistingMethodCall(
                    acceptorWithParameters,
                    new MethodCallAppendCallsVisitor<TService>(methodCallMetaData));
            }
        }

        private void AddMethodCallWithResultImpl(IMethodCallMetaData methodCallMetaData, IAcceptorWithParameters<IMethodCallWithResultVisitor<TService>> acceptorWithParameters )
        {
            var existingMethodInfoMetaData = FindExistingMethodCallMetaData(methodCallMetaData);
            if (existingMethodInfoMetaData == null)
            {
                _methodCalls.Add(methodCallMetaData);
            }
            else
            {
                AssertCallbackType(methodCallMetaData, existingMethodInfoMetaData);                
                AcceptExistingMethodCall(
                        acceptorWithParameters,
                        new MethodCallWithResultAppendCallsVisitor<TService>(methodCallMetaData));
            }
        }

        private static void AcceptExistingMethodCall<TAppendCallsVisitor,TMethodCallVisitor>(
            IAcceptorWithParameters<TMethodCallVisitor> existingMethodInfoMetaData,
            TAppendCallsVisitor appendCallsVisitor
            ) where TAppendCallsVisitor : TMethodCallVisitor
        {
            existingMethodInfoMetaData.Accept(appendCallsVisitor);
        }

        private static void AssertCallbackType(IMethodCallMetaData newMethodCallMetaData,
            IMethodCallMetaData existingMethodCallMetaData)
        {
            var newCallbackType = newMethodCallMetaData.CallbackType;
            var existingCallbackType = existingMethodCallMetaData.CallbackType;
            if (newCallbackType != existingCallbackType)
            {
                throw new NotSupportedException("Callback type may not be changed");
            }
        }

        private IMethodCallMetaData FindExistingMethodCallMetaData(IMethodCallMetaData methodCallMetaData)
        {
            var newRunMethodDescription = methodCallMetaData.RunMethodDescription;
            var existingMethodInfoMetaData =
                _methodCalls.FirstOrDefault(t => t.RunMethodDescription == newRunMethodDescription);
            return existingMethodInfoMetaData;
        }

        public TService GetService()
        {
            return _serviceFactory.CreateService(MethodCalls);          
        }

        public void AppendMethods(IHaveMethods<TService> otherMethods)
        {
            foreach (var otherMethod in otherMethods.MethodCalls)
            {
                AddMethodCall(otherMethod);
            }
        }
    }
}