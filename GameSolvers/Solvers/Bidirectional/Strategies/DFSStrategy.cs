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

        public int RemainingCount => _solutionsToSearch.Count;

        public void AddChildren(Board current)
        {
            if (CurrentDepthSearch < MaxDepthSearch)
            {
                CurrentDepthSearch++;

                foreach (var direction in SearchOrder.Where(current.PossibleMoves.Contains))
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
