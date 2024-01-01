using ComputeSharp;

namespace ArtificialIntelligence.NeuralNetworks
{
    public abstract class Tensor
    {
    }

    public class Tensor<T> : Tensor where T : unmanaged
    {
        public ReadWriteBuffer<T>? Buffer { get; set; } = null;
        
        public Tensor()
        {
        }
    }

}
