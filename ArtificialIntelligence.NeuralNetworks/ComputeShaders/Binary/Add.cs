using ComputeSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtificialIntelligence.NeuralNetworks.ComputeShaders.Binary
{
    [AutoConstructor]
    public readonly partial struct Add : IComputeShader
    {
        private readonly ReadWriteBuffer<float> left;
        private readonly ReadWriteBuffer<float> right;
        private readonly ReadWriteBuffer<float> dest;

        public void Execute()
        {
            int offset = 0;

            while ((ThreadIds.X + offset) < left.Length)
            {
                dest[ThreadIds.X + offset] = left[ThreadIds.X + offset] + right[ThreadIds.X + offset];
                offset += DispatchSize.Count;
            }
        }
    }
}
