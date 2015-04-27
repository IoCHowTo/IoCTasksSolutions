namespace UnityChildContainers
{
    public interface IOperation
    {
        int Evaluate(int a, int b);
    }

    public interface IOperationPlus : IOperation
    {
    }

    public interface IOperationMinus : IOperation
    {
    }


    public class Plus : IOperationPlus
    {
        public int Evaluate(int a, int b)
        {
            return a + b;
        }
    }

    public class Minus : IOperationMinus
    {
        public int Evaluate(int a, int b)
        {
            return a - b;
        }
    }
}