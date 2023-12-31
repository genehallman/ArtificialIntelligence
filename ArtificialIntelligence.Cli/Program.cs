using ComputeSharp;
using ComputeSharp.Interop;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace ArtificialIntelligence.Cli
{
    public class Program
    {
        static int MAXITERS = (int)(Math.Pow(2, 22) - Math.Pow(2, 6));
        static int MAXTHREADS = 1024;
        static int MAXFLOATSIZE = 1073676288;

        static void Device_DeviceLost(object? sender, DeviceLostEventArgs e)
        {
            Console.WriteLine($"ERROR: ${e.Reason}");
        }

        static void Log<T>(T[] values)
        {
            Console.WriteLine(string.Join(" ", values.Select(v => $"{v}".PadLeft(5))));
            Console.WriteLine("");
        }

        static void Main(string[] args)
        {
            try
            {
                //var nn = NeuralNetwork.Create(new
                //    {
                //        l1 = new Linear(10, 10),
                //    },
                //    (ctx, x) =>
                //    {
                //        x = ctx.l1.Run(x);
                //    }
                //);

                //var a = TensorOperation.From(new float[5] { 1, 2, 3, 4, 5 });
                //var b = TensorOperation.From(new float[5] { 1, 2, 3, 4, 5 });

                //var exp = TensorOperation.Add(a,b);

                //IRuntime runtime = new CpuRuntime();
                //runtime.Run(exp);

                //ShaderInfo shaderInfo = ReflectionServices.GetShaderInfo<Sum>();
                //Console.WriteLine(shaderInfo.HlslSource);


                var initNum = Random.Shared.NextSingle();
                var size = MAXFLOATSIZE;
                var iters = (size / 2) > MAXTHREADS ? MAXTHREADS : (size / 2);

                float[] source = new float[1];

                var device = GraphicsDevice.GetDefault();
                device.DeviceLost += Device_DeviceLost;
                ReadWriteBuffer<float> bufferSource = device.AllocateReadWriteBuffer<float>(size);
                GraphicsDevice.GetDefault().For(iters, 1, 1, iters, 1, 1, new Initialize(bufferSource, initNum));
                GraphicsDevice.GetDefault().For(iters, 1, 1, iters, 1, 1, new Sum(bufferSource));

                bufferSource.CopyTo(source, 0, 0, 1);
                Log(source);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }

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

    [AutoConstructor]
    public readonly partial struct Initialize : IComputeShader
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
