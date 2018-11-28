using Model;

namespace GameSolvers.Solvers.Metrics
{
    public class HammingMetricCalculator : IMetricCalculator
    {
        public int CalculatePriority(Board board)
        {
            int distanceSum = 0;
            int correctValue = 1;
            for (int i = 0; i < board.YLength; i++)
            {
                for (int j = 0; j < board.XLength; j++)
                {
                    distanceSum += board.Fields[i][j] == correctValue ? 0 : 1;
                    correctValue++;
                }
            }
            //'0' value correction
            distanceSum--;
            distanceSum += board.Fields[board.YLength - 1][board.XLength - 1] == 0 ? 0 : 1;

            return distanceSum;
        }
    }
}
