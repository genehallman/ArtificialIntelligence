using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ArtificialIntelligence.NeuralNetworks.Layers
{
    public class Linear
    {
        public Tensor Weights { get; set; }
        public Tensor Biases { get; set; }
        public Tensor Outputs { get; set; }
        public Func<int, double> initializer { get; set; } = (i) => Random.Shared.NextDouble();

        public Linear(int inputCount, int outputCount) 
        {
            Weights = new Tensor(inputCount, outputCount);
            Biases = new Tensor(outputCount); 
            Outputs = new Tensor(outputCount);

            for (int i = 0; i < Weights.Size[0]; i++)
            {
                for (int j = 0; j < Weights.Size[1]; j++)
                {
                    Weights[i, j] = initializer(i);
                }
            }
        }

        public Tensor Run(TensorOperation inputs)
        {
            for (int i = 0; i < Outputs.Size[0]; i++)
            {
                Outputs[i] = 0;

                for (int j = 0; j < inputs.Size[0]; j++)
                {
                    Outputs[i] = inputs[i] * Weights[i, j];
                }

                Outputs[i] += Biases[i];
            }

            return Outputs;
        }

        public static Expression Multiply(Tensor a, Tensor b)
        {
            return Expression.Multiply(Expression.Constant(a), Expression.Constant(b));
        }
    }
}
