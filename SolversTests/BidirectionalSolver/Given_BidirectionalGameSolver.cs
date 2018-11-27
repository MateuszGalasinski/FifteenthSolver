using GameSolvers.Solvers;

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
                    Solver = new BidirectionalBaseSolver(new ManhattanMetricStrategy(), new ManhattanMetricStrategy());
                    break;
            }
        }
    }
}
