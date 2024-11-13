
namespace cross_platform_mkr_1
{
    public class FilesHandler
    {
        private readonly string _inputFilePath;
        private readonly string _outputFilePath;

        public FilesHandler()
        {
            string baseDirectory = Directory.GetCurrentDirectory();
            string filesFolder = "files";

            if (baseDirectory.Contains("Debug"))
            {
                _inputFilePath = Path.Combine(baseDirectory, "..", "..", "..", filesFolder, "INPUT.TXT");
                _outputFilePath = Path.Combine(baseDirectory, "..", "..", "..", filesFolder, "OUTPUT.TXT");
            }
            else
            {
                _inputFilePath = Path.Combine(baseDirectory, filesFolder, "INPUT.TXT");
                _outputFilePath = Path.Combine(baseDirectory, filesFolder, "OUTPUT.TXT");
            }
        }

        public string InputFilePath
        {
            get { return _inputFilePath; }
        }

        public string OutputFilePath
        {
            get { return _outputFilePath; }
        }

        public virtual (int N, int A, int B) ReadInputLine(string line)
        {
            string[] fileContents = line.Trim().Split();
            return (int.Parse(fileContents[0]), int.Parse(fileContents[1]), int.Parse(fileContents[2]));
        }

        public virtual bool IsValuesValid(int N, int A, int B)
        {
            return 1 <= N && N <= 20 && 0 <= A && A <= 20 && 0 <= B && B <= 20;
        }
    }
}