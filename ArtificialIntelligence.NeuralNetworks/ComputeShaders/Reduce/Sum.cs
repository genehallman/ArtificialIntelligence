using ComputeSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtificialIntelligence.NeuralNetworks.ComputeShaders.Reduce
{
    [AutoConstructor]
    public readonly partial struct Sum : IComputeShader
    {
        private readonly ReadWriteBuffer<float> source;

        public void Execute()
        {
            int active = DispatchSize.Count;
            int size = source.Length;

            while (size > 1)
            {
                int offset = active;

                while ((ThreadIds.X + offset) < size)
                {
                    source[ThreadIds.X] += source[offset + ThreadIds.X];
                    offset += active;
                }

                Hlsl.AllMemoryBarrierWithGroupSync();

                size = active;
                active = (int)Hlsl.Ceil(active / 2f);
            }
        }
    }
}
