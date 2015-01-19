using System;

namespace IoCWithoutUnity
{
    /// <summary>
    /// This interface has no purpose really, but in case
    /// we would implement different IoC container than
    /// <see cref="Container"/>, it would be fairly easy to
    /// switch to this new implementation.
    /// </summary>
    public interface IContainer
    {
        object Create(string typeName);
    }

    /// <summary>
    /// Creation of all objects is encapsulated within this class
    /// and makes it an IoC container.
    /// </summary>
    public class Container : IContainer
    {
        /// <summary>
        /// You will have to cast return value of this method
        /// in order to do something useful with it, but that
        /// doesn't matter at the moment.
        /// </summary>
        public object Create(string typeName)
        {
            switch (typeName)
            {
                case "IOperationPlus":
                    return new Plus();
                case "IOperationMinus":
                    return new Minus();
                case "IOperationFactory":
                    return new OperationFactory(this);
                case "ICalculator":
                    return new Calculator((IOperationFactory)Create("IOperationFactory"));
                default:
                    throw new NotImplementedException();
            }
        }
    }
}