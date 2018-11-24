using Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameSolvers.Solvers
{

    public class HammingMetricSolver : BaseSolver
    {
        private readonly SortedList<int, (Board board, int depth)> _solutionsToSearch = new SortedList<int, (Board, int depth)>(new DuplicateKeyComparer<int>());

        protected override bool HasRemainingChild()
        {
            return _solutionsToSearch.Count != 0;
        }

        protected override void AddChildren((Board board, int depth) current)
        {
            foreach (var direction in current.board.PossibleMoves)
            {
                Board newBoard = current.board.GenerateChild(direction);
                _solutionsToSearch.Add(CalculatePriority(newBoard), (newBoard, CurrentDepthSearch));
            }
        }

        protected override (Board board, int depth) GetNextChild()
        {
            while (CheckedBoards.Contains(_solutionsToSearch.First().Value.board))
            {
                _solutionsToSearch.RemoveAt(0);
            }
            var first = _solutionsToSearch.First();
            _solutionsToSearch.RemoveAt(0);
            return first.Value;
        }

        private int CalculatePriority(Board board)
        {
            int distanceSum = 0;
            int correctValue = 1;
            for (int i = 0; i < board.YLength; i++)
            {
                for (int j = 0; j < board.XLength - 1; j++) // last should be handled separately
                {
                    distanceSum += board.Fields[j][i] == correctValue ? 0 : 1;
                    correctValue++;
                }
            }
            distanceSum += board.Fields[board.YLength - 1][board.XLength - 1] == 0 ? 0 : 1;

            return distanceSum;
        }

        private class DuplicateKeyComparer<TKey> : IComparer<TKey> where TKey : struct, IComparable
        {

            public int Compare(TKey x, TKey y)
            {
                int result = x.CompareTo(y);

                if (result == 0)
                    return 1;  
                else
                    return result;
            }
        }
    }

}
