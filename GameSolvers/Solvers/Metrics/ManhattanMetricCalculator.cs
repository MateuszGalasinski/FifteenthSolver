using Model;
using System;

namespace GameSolvers.Solvers.Metrics
{
    public class ManhattanMetricCalculator : IMetricCalculator
    {
        public int CalculatePriority(Board board)
        {
            int distanceSum = 0;
            for (int i = 0; i < board.YLength; i++)
            {
                for (int j = 0; j < board.XLength; j++)
                {
                    distanceSum += CalculateDistance(board, (i, j));
                }
            }

            return distanceSum;
        }

        private int CalculateDistance(Board board, (int y, int x) index)
        {
            int valueAtIndex = board.Fields[index.y][index.x] - 1;
            if (valueAtIndex != -1)
            {
                int correctY = valueAtIndex / board.YLength;
                int correctX = valueAtIndex % board.YLength;
                return Math.Abs(index.y - correctY) + Math.Abs(index.x - correctX);
            }
            else
            {
                return Math.Abs(index.y + 1 - board.YLength) + Math.Abs(index.x + 1 - board.XLength);
            }
        }
    }
}
