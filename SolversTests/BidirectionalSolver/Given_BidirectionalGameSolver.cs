using GameSolvers.Solvers;
using GameSolvers.Solvers.Bidirectional;
using GameSolvers.Solvers.Bidirectional.Strategies;
using Model;
using System.Collections.Generic;
using GameSolvers.Solvers.Metrics;

namespace SolversTests.BidirectionalSolver
{
    public class Given_BidirectionalGameSolver
    {
        protected BidirectionalBaseSolver Solver;

        public void With_GameSolver(string solverName)
        {
            switch (solverName)
            {
                case "Manhattan":
                    Solver = new BidirectionalBaseSolver(new MetricStrategy(new ManhattanMetricCalculator()), new MetricStrategy(new ManhattanMetricCalculator()));
                    break;
                case "BFS":
                    Solver = new BidirectionalBaseSolver(
                        new BFSStrategy(
                            new List<Direction>()
                            {
                                Direction.Right, Direction.Down, Direction.Left, Direction.Top 
                            }), 
                        new BFSStrategy(
                            new List<Direction>()
                            {
                                Direction.Top, Direction.Left, Direction.Down, Direction.Right
                            }));
                    break;
                case "DFS":
                    Solver = new BidirectionalBaseSolver(
                        new DFSStrategy(
                            new List<Direction>()
                            {
                                Direction.Right, Direction.Down, Direction.Left, Direction.Top
                            }, 30), 
                        new DFSStrategy(
                            new List<Direction>()
                            {
                                Direction.Top, Direction.Left, Direction.Down, Direction.Right
                            }, 30));
                    break;
            }
        }
    }
}
