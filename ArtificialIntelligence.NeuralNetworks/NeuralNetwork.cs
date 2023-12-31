using System.Dynamic;

namespace ArtificialIntelligence.NeuralNetworks
{
    public static class NeuralNetwork
    {
        public static NeuralNetwork<T> Create<T>(T ctx, Action<T, Tensor> run) where T : class
        {
            return new NeuralNetwork<T>(ctx, run);
        }
    }

    public class NeuralNetwork<T> where T : class
    {
        public T Context { get; set; }
        public Action<T, Tensor> Run { get; set; }

        
        public NeuralNetwork(T context, Action<T, Tensor> run) { 
            Context = context;
            Run = run;
        }
    }
}
