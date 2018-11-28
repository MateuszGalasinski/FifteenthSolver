using FluentAssertions;
using Model;
using NUnit.Framework;
using System.Collections.Generic;

namespace SolversTests.BidirectionalSolver
{
    public class BidirectionalSolverTests : Given_BidirectionalGameSolver
    {
        [TestCase("DFS")]
        [TestCase("BFS")]
        [TestCase("Manhattan")]
        public void OneStep_ShouldBeSolved(string solverName)
        {
            With_GameSolver(solverName);
            Board board = new Board(3, 3, new int[][]
            {
                new int[] {1, 2, 3},
                new int[] {4, 5, 6},
                new int[] {7, 0, 8}
            });

            var history = Solver.Solve(board);

            history.Moves.Should().BeEquivalentTo(new List<Direction>(new []{ Direction.Right }));
        }

        [TestCase("DFS")]
        [TestCase("BFS")]
        [TestCase("Manhattan")]
        public void TwoSteps_ShouldBeSolved(string solverName)
        {
            With_GameSolver(solverName);
            Board board = new Board(3, 3, new int[][]
            {
                new int[] {1, 2, 3},
                new int[] {4, 5, 6},
                new int[] {0, 7, 8}
            });

            var history = Solver.Solve(board);

            history.Moves.Should().BeEquivalentTo(new List<Direction>(new[] { Direction.Right, Direction.Right }));
        }

        [TestCase("DFS")]
        [TestCase("BFS")]
        [TestCase("Manhattan")]
        public void ManySteps_ShouldBeSolved(string solverName)
        {
            With_GameSolver(solverName);
            Board board = new Board(3, 3, new int[][]
            {
                new int[] {1, 2, 3},
                new int[] {0, 4, 5},
                new int[] {7, 8, 6},
            });

            var history = Solver.Solve(board);

            history.Moves.Should().BeEquivalentTo(new List<Direction>(new[] { Direction.Right, Direction.Right, Direction.Down}));
        }

        [TestCase("DFS")]
        [TestCase("BFS")]
        [TestCase("Manhattan")]
        public void ManySteps_Size4_ShouldBeSolved(string solverName)
        {
            With_GameSolver(solverName);
            Board board = new Board(4, 4, new int[][]
            {
                new int[] {1, 2, 3, 4},
                new int[] {5, 6, 7, 8},
                new int[] {9, 10, 11, 12},
                new int[] {0, 13, 14, 15},
            });

            var history = Solver.Solve(board);

            history.Moves.Should().BeEquivalentTo(new List<Direction>(new[] { Direction.Right, Direction.Right, Direction.Right }));
        }
    }
}
