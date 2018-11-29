using GameSolvers.Solvers.Unidirectional.Base;
using Model;
using System.Collections.Generic;
using System.Linq;

namespace GameSolvers.Solvers.Unidirectional
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
            if (current.MovesHistory.Count < MaxDepthSearch)
            {
                List<Direction> moves = current.PossibleMoves;
                Solution.ProcessedStatesCounter += moves.Count;

                foreach (var direction in SearchOrder.Where(moves.Contains))
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
            return _solutionsToSearch.Pop();
        }
    }
}
