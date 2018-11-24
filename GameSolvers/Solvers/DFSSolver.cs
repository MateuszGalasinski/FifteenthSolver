using Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameSolvers.Solvers
{
    public class DFSSolver : BaseSolver
    {
        private Stack<Board> _solutionsToSearch = new Stack<Board>();

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

                foreach (var direction in current.PossibleMoves.OrderBy(d => SearchOrder.IndexOf(d))) //use possible moves in order given in _searchOrder
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
