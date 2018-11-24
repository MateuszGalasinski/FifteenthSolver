using Data;
using FluentAssertions;
using GameSolvers.Solvers;
using Model;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;

namespace DataTests
{
    public class WriterTests
    {
        [Test]
        public void TestToRunWriter()
        {
            DFSSolver Solver = new DFSSolver(
                new List<Direction>()
                {
                    Direction.Right, Direction.Down, Direction.Left, Direction.Top ,

                }, 30);

            Board board = new Board(4, 4, new int[][]
            {
                new int[] {1, 2, 3, 4},
                new int[] {5, 6, 7, 8},
                new int[] {9, 10, 11, 12},
                new int[] {0, 13, 14, 15},
            });

            Board newBoard = Solver.Solve(board);

            newBoard.IsSolved().Should().BeTrue();

            Writer writer = new Writer();
            string solutionFilePath = Path.Combine(
                Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName,
                @"testSolution.txt");
            string additionalFilePath = Path.Combine(
                Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName,
                @"testAdditionalInfo.txt");
            writer.WriteSolution(solutionFilePath, additionalFilePath, Solver.SolutionMetadata);
        }
    }
}
