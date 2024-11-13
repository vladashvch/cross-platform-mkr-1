using System.Numerics;

namespace cross_platform_mkr_1
{
    public static class Program
    {
        static void Main()
        {
            try
            {
                Console.WriteLine("MKR-1");
                Console.WriteLine();
                ExecuteProgram();

            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
        }
        public static void ExecuteProgram()
        {
            var filesHandler = new FilesHandler();

            string inputFilePath = filesHandler.InputFilePath;
            string outputFilePath = filesHandler.OutputFilePath;

            List<string> answers = ProcessInputFile(inputFilePath, filesHandler);

            File.WriteAllText(outputFilePath, string.Join(Environment.NewLine, answers).Trim());
        }

        public static List<string> ProcessInputFile(string inputFilePath, FilesHandler filesHandler)
        {
            List<string> answers = new List<string>();
            string[] lines = File.ReadAllLines(inputFilePath);

            foreach (string line in lines)
            {
                (int N, int A, int B) = filesHandler.ReadInputLine(line);

                if (!filesHandler.IsValuesValid(N, A, B))
                {
                    Console.WriteLine("The values in the file do not match the condition.");
                    return answers;
                }

                Console.WriteLine($"Received values N: {N}, A: {A}, B: {B}");

                string result = Calculate(N, A, B).ToString();
                Console.WriteLine($"Answer: the number of ways is {result}");
                Console.WriteLine();

                answers.Add(result);
            }

            return answers;
        }

        public static BigInteger Calculate(int N, int A, int B)
        {
            BigInteger cNa = BinomialCoefficient(N + A, A);
            BigInteger cNb = BinomialCoefficient(N + B, B);
            BigInteger result = cNa * cNb;

            Console.WriteLine($"So: C(N+A, A) * C(N+B, B) = {cNa} * {cNb} = {result}");
            return result;
        }

        public static BigInteger BinomialCoefficient(int N, int K)
        {
            BigInteger result = 1;
            if (K > N) return 0;
            if (N == K) { return 1; }
            for (int i = 1; i <= K; i++)
            {
                result *= N--;
                result /= i;

                Console.WriteLine($"Step {i}: result = {result}, n - (k - {i}) = {N - (K - i)}, i = {i}");
            }

            return result;
        }
    }
}
