using ArtificialIntelligence.NeuralNetworks.ComputeShaders.Reduce;
using ArtificialIntelligence.NeuralNetworks.ComputeShaders.Unary;
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
            TestComputeShader();
        }

        static void Goal()
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

        }

        static void TestComputeShader()
        {
            try
            {
                var initNum = Random.Shared.NextSingle();
                var size = MAXFLOATSIZE;
                var iters = (size / 2) > MAXTHREADS ? MAXTHREADS : (size / 2);

                float[] source = new float[1];

                var device = GraphicsDevice.GetDefault();
                device.DeviceLost += Device_DeviceLost;
                ReadWriteBuffer<float> bufferSource = device.AllocateReadWriteBuffer<float>(size);
                GraphicsDevice.GetDefault().For(iters, 1, 1, iters, 1, 1, new Const(bufferSource, initNum));
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
}
