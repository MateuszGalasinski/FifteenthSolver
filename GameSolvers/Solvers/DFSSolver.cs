using Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameSolvers.Solvers
{
    public class DFSSolver : BaseSolver
    {
        private Stack<(Board board, int depth)> _solutionsToSearch = new Stack<(Board, int depth)>();

        public DFSSolver(List<Direction> searchOrder, int maxDepthSearch)
        {
            SearchOrder = searchOrder;
            SearchOrder.Reverse(); // adding to stack should go in reversed order

            MaxDepthSearch = maxDepthSearch;
        }

        protected override void AddChildren((Board board, int depth) current)
        {
            if (CurrentDepthSearch < MaxDepthSearch)
            {
                CurrentDepthSearch++;
                SolutionMetadata.MaxRecursion = Math.Max(SolutionMetadata.MaxRecursion, CurrentDepthSearch);

                foreach (var direction in current.board.PossibleMoves.OrderBy(d => SearchOrder.IndexOf(d))) //use possible moves in order given in _searchOrder
                {
                    Board newBoard = current.board.GenerateChild(direction);
                    _solutionsToSearch.Push((newBoard, CurrentDepthSearch));
                }
            }
        }

        protected override bool HasRemainingChild()
        {
            return _solutionsToSearch.Count != 0;
        }

        protected override (Board board, int depth) GetNextChild()
        {
            while (CheckedBoards.Contains(_solutionsToSearch.Peek().board))
            {
                _solutionsToSearch.Pop();
            }
            return _solutionsToSearch.Pop();
        }
    }
}
