namespace ArtificialIntelligence.NeuralNetworks
{
    public class Tensor
    {
        public int[] Size { get; private set; }

        public Tensor(params int[] sizes)
        {
            Size = sizes;
        }

        public dynamic this[params int[] indexes] { get => null; set { } }
    }

}
