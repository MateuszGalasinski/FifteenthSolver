using Model;

namespace GameSolvers.Solvers.Metrics
{
    public interface IMetricCalculator
    {
        int CalculatePriority(Board board);
    }
}