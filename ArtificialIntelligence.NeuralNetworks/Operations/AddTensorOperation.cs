namespace ArtificialIntelligence.NeuralNetworks.Operations
{
    public class AddTensorOperation : TensorOperation
    {
        public TensorOperation Left { get; set; }
        public TensorOperation Right { get; set; }

        public override Tensor Forward()
        {
            return null;
        }

        public override Tensor Backward(Tensor val, string name = null)
        {
            return null;
        }
    }
}
