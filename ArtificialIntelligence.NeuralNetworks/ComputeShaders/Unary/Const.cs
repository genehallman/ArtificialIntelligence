using ComputeSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtificialIntelligence.NeuralNetworks.ComputeShaders.Unary
{
    [AutoConstructor]
    public readonly partial struct Const : IComputeShader
    {
        private readonly ReadWriteBuffer<float> source;
        private readonly float value;

        public void Execute()
        {
            int offset = 0;

            while ((ThreadIds.X + offset) < source.Length)
            {
                source[ThreadIds.X + offset] = value;
                offset += DispatchSize.Count;
            }
        }
    }
}
