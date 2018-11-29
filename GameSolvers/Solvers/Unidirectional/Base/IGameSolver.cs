using Model;

namespace GameSolvers.Solvers.Unidirectional.Base
{
    public interface IGameSolver
    {
        Solution Solve(Board board);
    }
}
