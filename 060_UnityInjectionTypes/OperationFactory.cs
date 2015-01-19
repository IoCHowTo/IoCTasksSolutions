using System;
using Microsoft.Practices.Unity;

namespace UnityInjectionTypes
{
    public interface IOperationFactory
    {
        IOperation Create(string token);
    }

    public class OperationFactory : IOperationFactory
    {
        private IOperationMinus _minus;
        private IOperationPlus _plus;
        private IOperationDiv _div;

        public OperationFactory(IOperationMinus minus, IOperationPlus plus, IOperationDiv div)
        {
            _minus = minus;
            _plus = plus;
            _div = div;
        }

        #region This can be ommited for the injection factory
        // This is needed just because the solution also demonstrates usage of a factory
        [InjectionConstructor]
        public OperationFactory(IOperationMinus minus)
        {
            _minus = minus;
        }

        [Dependency]
        public IOperationPlus Plus
        {
            get { return _plus; }
            set { _plus = value; }
        }

        [InjectionMethod]
        public void Setup(IOperationDiv div)
        {
            _div = div;
        }
        #endregion

        public IOperation Create(string token)
        {
            switch (token)
            {
                case "-":
                    return _minus;
                case "+":
                    return _plus;
                case "/":
                    return _div;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}