namespace ArtificialIntelligence.NeuralNetworks.Operations
{
    public enum Operation
    {
        Contiguous,
        ContiguousBackward,
        Cast,
        Zero,
        Neg,
        Sin,
        Relu,
        Log,
        Exp,
        Sqrt,
        Sigmoid,
        Less,
        Eq,
        Xor,
        Add,
        Sub,
        Mul,
        Div,
        Where,
        Sum,
        Max,
        Expand,
        Reshape,
        Permute,
        Pad,
        Shrink,
        Flip
    }

    public abstract class TensorOperation
    {
        public abstract Tensor Forward();
        public abstract Tensor Backward(Tensor val, string name = null);
    }
}
