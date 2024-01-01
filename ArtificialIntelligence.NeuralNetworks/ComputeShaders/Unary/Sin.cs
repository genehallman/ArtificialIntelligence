using ComputeSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtificialIntelligence.NeuralNetworks.ComputeShaders.Unary
{
    [AutoConstructor]
    public readonly partial struct Sin : IComputeShader
    {
        private readonly ReadWriteBuffer<float> source;

        public void Execute()
        {
            int offset = 0;

            while ((ThreadIds.X + offset) < source.Length)
            {
                source[ThreadIds.X + offset] = Hlsl.Exp2(source[ThreadIds.X + offset]);
                offset += DispatchSize.Count;
            }
        }
    }
}
