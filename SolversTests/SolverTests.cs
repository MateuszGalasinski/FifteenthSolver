﻿using FluentAssertions;
using Model;
using NUnit.Framework;

namespace SolversTests
{
    public class SolverTests : Given_GameSolver
    {
        [TestCase("BFT")]
        [TestCase("DFT")]
        [TestCase("Manhattan")]
        [TestCase("Hamming")]
        public void OneStep_ShouldBeSolved(string solverName)
        {
            With_GameSolver(solverName);
            Board board = new Board(3, 3, new int[][]
            {
                new int[] {1, 2, 3},
                new int[] {4, 5, 6},
                new int[] {7, 0, 8},
            });

            Board newBoard = Solver.Solve(board);

            newBoard.IsSolved().Should().BeTrue();
        }

        [TestCase("BFT")]
        [TestCase("DFT")]
        [TestCase("Manhattan")]
        [TestCase("Hamming")]
        public void TwoSteps_ShouldBeSolved(string solverName)
        {
            With_GameSolver(solverName);
            Board board = new Board(3, 3, new int[][]
            {
                new int[] {1, 2, 3},
                new int[] {4, 5, 6},
                new int[] {0, 7, 8},
            });

            Board newBoard = Solver.Solve(board);

            newBoard.IsSolved().Should().BeTrue();
        }

        [TestCase("BFT")]
        [TestCase("DFT")]
        [TestCase("Manhattan")]
        [TestCase("Hamming")]
        public void ManySteps_ShouldBeSolved(string solverName)
        {
            With_GameSolver(solverName);
            Board board = new Board(3, 3, new int[][]
            {
                new int[] {1, 2, 3},
                new int[] {0, 4, 5},
                new int[] {7, 8, 6},
            });

            Board newBoard = Solver.Solve(board);

            newBoard.IsSolved().Should().BeTrue();
        }

        [TestCase("BFT")]
        [TestCase("DFT")]
        [TestCase("Manhattan")]
        [TestCase("Hamming")]
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

            Board newBoard = Solver.Solve(board);

            newBoard.IsSolved().Should().BeTrue();
        }
    }
}