﻿using GameSolvers.Solvers.Base;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameSolvers.Solvers
{
    public class DFSSolver : BaseSolver
    {
        private readonly Stack<Board> _solutionsToSearch = new Stack<Board>();
        protected List<Direction> SearchOrder;

        public DFSSolver(List<Direction> searchOrder, int maxDepthSearch)
        {
            SearchOrder = searchOrder;
            SearchOrder.Reverse(); // adding to stack should go in reversed order

            MaxDepthSearch = maxDepthSearch;
        }

        protected override void AddChildren(Board current)
        {
            if (CurrentDepthSearch < MaxDepthSearch)
            {
                CurrentDepthSearch++;
                Solution.MaxRecursion = Math.Max(Solution.MaxRecursion, current.MovesHistory.Count);

                foreach (var direction in SearchOrder.Where(current.PossibleMoves.Contains))
                {
                    _solutionsToSearch.Push(current.GenerateChild(direction));
                }
            }
        }

        protected override bool HasRemainingChild()
        {
            return _solutionsToSearch.Count != 0;
        }

        protected override Board GetNextChild()
        {
            while (CheckedBoards.Contains(_solutionsToSearch.Peek()))
            {
                _solutionsToSearch.Pop();
            }
            return _solutionsToSearch.Pop();
        }
    }
}
