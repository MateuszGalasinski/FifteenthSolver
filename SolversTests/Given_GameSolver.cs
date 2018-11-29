using GameSolvers.Solvers.Metrics;
using GameSolvers.Solvers.Unidirectional;
using GameSolvers.Solvers.Unidirectional.Base;
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
                    Solver = new BFSSolver(new List<Direction>()
                    {
                        Direction.Right, Direction.Down, Direction.Left, Direction.Top ,

                    });
                    break;
                case "DFT":
                    Solver = new DFSSolver(
                        new List<Direction>()
                        {
                            Direction.Left, Direction.Top, Direction.Down, Direction.Right ,

                        }, 25);
                    break;
                case "Manhattan":
                    Solver = new MetricSolver(new ManhattanMetricCalculator());
                    break;
                case "Hamming":
                    Solver = new MetricSolver(new HammingMetricCalculator());
                    break;
            }
        }
    }
}
