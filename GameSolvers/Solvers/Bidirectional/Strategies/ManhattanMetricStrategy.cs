using GameSolvers.Solvers.Bidirectional.Strategies.Base;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameSolvers.Solvers.Bidirectional.Strategies
{

    public class ManhattanMetricStrategy : BaseStrategy, IStrategy
    {
        private readonly SortedList<int, Board> _solutionsToSearch = new SortedList<int, Board>(new DuplicateKeyComparer<int>());

        public bool HasRemainingChild()
        {
            return _solutionsToSearch.Count != 0;
        }

        public int RemainingCount => _solutionsToSearch.Count;

        public void AddChildren(Board current)
        {
            foreach (var direction in current.PossibleMoves)
            {
                Board newBoard = current.GenerateChild(direction);
                _solutionsToSearch.Add(CalculatePriority(newBoard), newBoard);
            }
        }

        public Board GetNextChild()
        {
            while (CheckedBoards.Contains(_solutionsToSearch.First().Value))
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
                return Math.Abs(index.y - board.YLength) + Math.Abs(index.x - board.XLength); // TODO: check if it shouldn't be -1
            }
        }
    }

}
