using Model;

namespace GameSolvers.Solvers.Base
{
    public interface IGameSolver
    {
        Solution Solve(Board board);
    }
}
