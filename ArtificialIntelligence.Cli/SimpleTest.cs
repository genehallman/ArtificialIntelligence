namespace ArtificialIntelligence.Cli
{
    internal class SimpleTest
    {
        static float lr = 1;
        static float bias = 1;
        static float[] weights = new float[3] { Random.Shared.NextSingle(), Random.Shared.NextSingle(), Random.Shared.NextSingle() };

        static void TrainAndTest(string[] args)
        {
            // training

            for (var i = 0; i < 50; i++)
            {
                TrainPerceptron(1, 1, 1);
                TrainPerceptron(1, 0, 0);
                TrainPerceptron(0, 1, 1);
                TrainPerceptron(0, 0, 0);
            }

            // testing
            Console.WriteLine($"1 1 = {CalculateActivation(CalculateOutput(1, 1))}");
            Console.WriteLine($"1 0 = {CalculateActivation(CalculateOutput(1, 0))}");
            Console.WriteLine($"0 1 = {CalculateActivation(CalculateOutput(0, 1))}");
            Console.WriteLine($"0 0 = {CalculateActivation(CalculateOutput(0, 0))}");

        }

        static void TrainPerceptron(int input1, int input2, int output)
        {
            float outputP, error;

            outputP = CalculateOutput(input1, input2);
            outputP = CalculateActivation(outputP);

            error = output - outputP;
            weights[0] += error * input1 * lr;
            weights[1] += error * input2 * lr;
            weights[2] += error * bias * lr;
        }

        static float CalculateActivation(float outputP) => outputP > 0 ? 1 : 0;

        static float CalculateOutput(int input1, int input2) => input1 * weights[0] + input2 * weights[1] + bias * weights[2];
    }
}
