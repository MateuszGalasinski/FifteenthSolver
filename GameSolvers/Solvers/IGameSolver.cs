using Model;

namespace GameSolvers.Solvers
{
    public interface IGameSolver
    {
        Solution Solution { get; set; }
        Board Solve(Board board);
    }
}
