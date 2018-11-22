﻿using GameSolvers.Solvers;
using Model;
using System.Collections.Generic;

namespace SolversTests
{
    public class Given_GameSolver
    {
        protected IGameSolver Solver;

        public void With_GameSolver(string solverName)
        {
            switch (solverName)
            {
                case "BFT":
                    Solver = new BFTSolver();
                    break;
                case "DFT":
                    Solver = new DFTSolver(
                        new List<Direction>()
                        {
                            Direction.Right, Direction.Down, Direction.Left, Direction.Top ,

                        }, 30);
                    break;
            }
        }
    }
}
