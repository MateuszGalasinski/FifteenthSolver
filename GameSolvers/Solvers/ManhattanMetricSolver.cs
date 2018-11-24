﻿using Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameSolvers.Solvers
{

    public class ManhattanMetricSolver : BaseSolver
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
                if (!GeneratedBoards.Contains(newBoard))
                {
                    GeneratedBoards.Add(newBoard);
                    _solutionsToSearch.Add(CalculatePriority(newBoard), (newBoard, CurrentDepthSearch));
                }
            }
        }

        protected override (Board board, int depth) GetNextChild()
        {
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