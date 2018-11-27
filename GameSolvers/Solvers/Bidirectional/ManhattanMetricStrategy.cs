using GameSolvers.Solvers.Bidirectional;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameSolvers.Solvers
{

    public class ManhattanMetricStrategy : IStrategy
    {
        public readonly HashSet<Board> CheckedBoards = new HashSet<Board>(new BoardValuesEqualityComparer());
        public readonly SortedList<int, Board> SolutionsToSearch = new SortedList<int, Board>(new DuplicateKeyComparer<int>());

        public bool HasRemainingChild()
        {
            return SolutionsToSearch.Count != 0;
        }

        public void AddChildren(Board current)
        {
            foreach (var direction in current.PossibleMoves)
            {
                Board newBoard = current.GenerateChild(direction);
                SolutionsToSearch.Add(CalculatePriority(newBoard), newBoard);
            }
        }

        public Board GetNextChild()
        {
            while (CheckedBoards.Contains(SolutionsToSearch.First().Value))
            {
                SolutionsToSearch.RemoveAt(0);
            }
            var first = SolutionsToSearch.First();
            SolutionsToSearch.RemoveAt(0);
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
