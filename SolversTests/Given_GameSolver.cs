using GameSolvers.Solvers;
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
                    Solver = new BFSSolver();
                    break;
                case "DFT":
                    Solver = new DFSSolver(
                        new List<Direction>()
                        {
                            Direction.Right, Direction.Down, Direction.Left, Direction.Top ,

                        }, 30);
                    break;
                case "Manhattan":
                    Solver = new ManhattanMetricSolver();
                    break;
                case "Hamming":
                    Solver = new HammingMetricSolver();
                    break;
            }
        }
    }
}
