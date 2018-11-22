using Model;

namespace GameSolvers.Solvers
{
    public interface IGameSolver
    {
        Board Solve(Board board);
    }
}
