namespace ArtificialIntelligence.NeuralNetworks.Runtimes
{
    public interface IRuntime
    {
        public Tensor Run(TensorOperation exp);
    }
}