namespace UnityNamedRegistratations
{
    public interface IOperation
    {
        int Evaluate(int a, int b);
    }
    
    public class Plus : IOperation
    {
        public int Evaluate(int a, int b)
        {
            return a + b;
        }
    }
    
    public class Minus : IOperation
    {
        public int Evaluate(int a, int b)
        {
            return a - b;
        }
    }
}