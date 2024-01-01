namespace ArtificialIntelligence.NeuralNetworks.Operations
{
    public class ParamTensorOperation : TensorOperation
    {
        public Tensor Source { get; set; }

        public override Tensor Forward()
        {
            return Source;
        }

        public override Tensor Backward(Tensor val, string name = null)
        {
            return null;
        }
    }
}
