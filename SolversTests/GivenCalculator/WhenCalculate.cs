using FluentAssertions;
using Model;
using NUnit.Framework;

namespace SolversTests.GivenCalculator
{
    class WhenCalculate : GivenCalculator
    {
        [TestCase("Manhattan", 0)]
        [TestCase("Hamming", 0)]
        public void Solved_ShouldBeZero(string solverName, int correctValue)
        {
            With_Calculator(solverName);
            Board board = new Board(3, 3, new int[][]
            {
                new int[] {1, 2, 3},
                new int[] {4, 5, 6},
                new int[] {7, 8, 0},
            });

            int result = Calculator.CalculatePriority(board);

            result.Should().Be(correctValue);
        }

        [TestCase("Manhattan", 6)]
        [TestCase("Hamming", 2)]
        public void TwoWrong(string solverName, int correctValue)
        {
            With_Calculator(solverName);
            Board board = new Board(3, 3, new int[][]
            {
                new int[] {1, 2, 3},
                new int[] {0, 5, 6},
                new int[] {7, 8, 4},
            });

            int result = Calculator.CalculatePriority(board);

            result.Should().Be(correctValue);
        }

        [TestCase("Manhattan", 16)]
        [TestCase("Hamming", 9)]
        public void ShiftedByOne(string solverName, int correctValue)
        {
            With_Calculator(solverName);
            Board board = new Board(3, 3, new int[][]
            {
                new int[] {0, 1, 2},
                new int[] {3, 4, 5},
                new int[] {6, 7, 8}
            });

            int result = Calculator.CalculatePriority(board);

            result.Should().Be(correctValue);
        }
    }
}
