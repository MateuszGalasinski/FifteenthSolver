using FluentAssertions;
using GameSolvers;
using Model;
using NUnit.Framework;

namespace SolversTests
{
    public class DFTSolverTests
    {
        private DFTSolver _solver;

        [SetUp]
        public void Setup()
        {
            _solver = new DFTSolver();
        }

        [Test]
        public void OneStep_ShouldBeSolved()
        {
            Board board = new Board(3, 3, new int[][]
            {
                new int[] {1, 2, 3},
                new int[] {4, 5, 6},
                new int[] {7, 0, 8},
            });

            Board newBoard = _solver.Solve(board);

            newBoard.IsSolved().Should().BeTrue();
        }

        [Test]
        public void TwoSteps_ShouldBeSolved()
        {
            Board board = new Board(3, 3, new int[][]
            {
                new int[] {1, 2, 3},
                new int[] {4, 5, 6},
                new int[] {0, 7, 8},
            });

            Board newBoard = _solver.Solve(board);

            newBoard.IsSolved().Should().BeTrue();
        }

        [Test]
        public void ManySteps_ShouldBeSolved()
        {
            Board board = new Board(3, 3, new int[][]
            {
                new int[] {1, 2, 3},
                new int[] {0, 4, 5},
                new int[] {7, 8, 6},
            });

            Board newBoard = _solver.Solve(board);

            newBoard.IsSolved().Should().BeTrue();
        }

        [Test]
        public void ManySteps_Size4_ShouldBeSolved()
        {
            Board board = new Board(4, 4, new int[][]
            {
                new int[] {1, 2, 3, 4},
                new int[] {5, 6, 0, 8},
                new int[] {9, 10, 7, 12},
                new int[] {13, 14, 11, 15},
            });

            Board newBoard = _solver.Solve(board);

            newBoard.IsSolved().Should().BeTrue();
        }
    }
}
