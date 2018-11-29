using GameSolvers.Solvers.Bidirectional.Strategies.Base;
using Model;
using System.Collections.Generic;
using System.Linq;

namespace GameSolvers.Solvers
{
    public class DFSStrategy : BaseStrategy, IStrategy
    {
        private readonly Stack<Board> _solutionsToSearch = new Stack<Board>();
        private int MaxDepthSearch { get; set; } = 1;
        protected List<Direction> SearchOrder;

        public DFSStrategy(List<Direction> searchOrder, int maxDepthSearch)
        {
            SearchOrder = searchOrder;
            SearchOrder.Reverse(); // adding to stack should go in reversed order

            MaxDepthSearch = maxDepthSearch;
        }

        public void AddChildren(Board current)
        {
            if (current.MovesHistory.Count < MaxDepthSearch)
            {
                List<Direction> moves = current.PossibleMoves;
                ProcessedStatesCounter += moves.Count;

                foreach (var direction in SearchOrder.Where(moves.Contains))
                {
                    _solutionsToSearch.Push(current.GenerateChild(direction));
                }
            }
        }

        public bool HasRemainingChild()
        {
            return _solutionsToSearch.Count != 0;
        }

        public Board GetNextChild()
        {
            while (CheckedBoards.Contains(_solutionsToSearch.Peek()))
            {
                _solutionsToSearch.Pop();
            }
            return _solutionsToSearch.Pop();
        }
    }
}
