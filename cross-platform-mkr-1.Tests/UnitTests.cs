using Moq;
using System.Numerics;

namespace cross_platform_mkr_1.Tests
{
    public class UnitTests
    {
        [Theory]
        [InlineData("5 10 15", 5, 10, 15)]
        [InlineData("0 1 3", 0, 1, 3)]
        public void ReadInputLine_ReturnsExpectedTuple(string input, int expectedN, int expectedA, int expectedB)
        {
            var filesHandler = new FilesHandler();
            var result = filesHandler.ReadInputLine(input);

            Assert.Equal((expectedN, expectedA, expectedB), result);
        }

        [Theory]
        [InlineData(0, 1, 3, false)]
        [InlineData(5, 10, 15, true)]
        [InlineData(-1, 2, 3, false)]
        [InlineData(3, 4, 5, true)]
        public void IsValuesValid_ReturnsExpectedValidationResult(int N, int A, int B, bool expectedResult)
        {
            var filesHandler = new FilesHandler();
            var result = filesHandler.IsValuesValid(N, A, B);

            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData(1, 1, 1, 4)]
        [InlineData(2, 1, 1, 9)]
        public void Calculate_ReturnsExpectedResultForGivenInputs(int N, int A, int B, long expectedResult)
        {
            BigInteger result = Program.Calculate(N, A, B);
            Assert.Equal((BigInteger)expectedResult, result);
        }

        [Theory]
        [InlineData(8, 3, 56)]
        [InlineData(7, 2, 21)]
        [InlineData(6, 3, 20)]
        [InlineData(5, 2, 10)]
        public void BinomialCoefficient_ReturnsExpectedValue(int n, int k, long expectedResult)
        {
            BigInteger result = Program.BinomialCoefficient(n, k);

            Assert.Equal((BigInteger)expectedResult, result);
        }

        [Fact]
        public void ProcessInputFile_ValidInput_ReturnsCorrectResults()
        {
            string inputFilePath = "testInput.txt";
            var testLines = new List<string>
            {
                "5,2,3",
                "4,1,2"
            };
            File.WriteAllLines(inputFilePath, testLines);

            var filesHandlerMock = new Mock<FilesHandler>();

            filesHandlerMock.Setup(fh => fh.ReadInputLine(It.IsAny<string>()))
                .Returns((string line) =>
                {
                    var parts = line.Split(',');
                    return (int.Parse(parts[0]), int.Parse(parts[1]), int.Parse(parts[2]));
                });

            filesHandlerMock.Setup(fh => fh.IsValuesValid(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(true);

            var result = Program.ProcessInputFile(inputFilePath, filesHandlerMock.Object);

            Assert.Equal(2, result.Count);
            Assert.NotEmpty(result[0]);
            Assert.NotEmpty(result[1]);

            File.Delete(inputFilePath);
        }
    }
}