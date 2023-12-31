using ArtificialIntelligence.NeuralNetworks;

namespace ArtificialIntelligence.NeuralNetworks
{
    /*
     Buffer                                                       # class of memory on this device
     unary_op  (NOOP, EXP2, LOG2, CAST, SIN, SQRT)                # A -> A
     reduce_op (SUM, MAX)                                         # A -> B (smaller size, B has 1 in shape)
     binary_op (ADD, SUB, MUL, DIV, CMPEQ, MAX)                   # A + A -> A (all the same size)
     movement_op (EXPAND, RESHAPE, PERMUTE, PAD, SHRINK, STRIDE)  # A -> B (different size)
     load_op   (EMPTY, CONST, FROM, CONTIGUOUS, CUSTOM)           # -> A   (initialize data on device)
     ternary_op (WHERE)                                           # A, A, A -> A
     ternary_op [[optional]] (MULACC)                             # A * A -> B
    */

    public enum Operation
    {
        Noop, Exp, Log, Cast, Sin, Sqrt,
        Sum, Max,
        Add, Sub, Mul, Div, Cmpeq, Max2,
        Expand, Reshape, Permute, Pad, Shrink, Stride,
        Empty, Const, From, Contiguous, Custom,
        Where,
        Mulacc
    }

    public abstract class TensorOperation
    {
        public Operation Operation { get; set; }
        public virtual int[] Size { get; }
        public dynamic this[params int[] indexes] { get => null; set { } }

        public static TensorOperation From(double[] source) => new FromTensorOperation { Operation = Operation.From, Source = source };
        public static TensorOperation Add(TensorOperation left, TensorOperation right) => new AddTensorOperation { Operation = Operation.Add, Left = left, Right = right };
    }

    public class AddTensorOperation : TensorOperation
    {
        public TensorOperation Left { get; set; }
        public TensorOperation Right { get; set; }
        public int[] Size { get => Left.Size; }
    }

    public class FromTensorOperation : TensorOperation
    {
        public double[] Source { get; set; }
        public int[] Size { get; private set; }
    }
}
