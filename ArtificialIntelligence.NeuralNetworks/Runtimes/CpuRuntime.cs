using System.Linq.Expressions;

namespace ArtificialIntelligence.NeuralNetworks.Runtimes
{
    public class CpuRuntime : IRuntime
    {
        public Tensor Run(TensorOperation exp)
        {
            switch (exp.Operation)
            {
                case Operation.From:
                    return new Tensor(exp.Size);

                case Operation.Add:
                    var op = exp as AddTensorOperation;
                    return new Tensor(exp.Size);

                    break;
            }
            return null;
        }
    }


}
