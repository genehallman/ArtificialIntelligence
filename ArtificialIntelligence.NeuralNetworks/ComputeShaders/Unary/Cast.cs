using ComputeSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtificialIntelligence.NeuralNetworks.ComputeShaders.Unary
{
    [AutoConstructor]
    public readonly partial struct Cast : IComputeShader
    {
        private readonly ReadWriteBuffer<float> source;

        public void Execute()
        {
            //Todo
        }
    }
}
